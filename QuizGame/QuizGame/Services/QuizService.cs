using Microsoft.EntityFrameworkCore;
using QuizGame.Data;
using QuizGame.Models;

namespace QuizGame.Services;

public class QuizService : IQuizService
{
    private readonly QuizDbContext _context;

    public QuizService(QuizDbContext context)
    {
        _context = context;
    }

    public async Task<List<Question>> GetQuizzesAsync()
    {
        return await _context.Questions
            .Include(q => q.Choices)
            .OrderBy(q => q.Id) // ID順に並べる
            .ToListAsync();
    }

    // ▼ 追加：IDで取得（選択肢も含める）
    public async Task<Question?> GetQuestionByIdAsync(long id)
    {
        return await _context.Questions
            .Include(q => q.Choices)
            .FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task AddQuizAsync(Question question)
    {
        _context.Questions.Add(question);
        await _context.SaveChangesAsync();
    }

    // ▼ 追加：更新ロジック
    public async Task UpdateQuestionAsync(Question updatedQuestion)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            // 1. DBから現在のデータを「選択肢」も含めて取得
            var existingQuestion = await _context.Questions
                .Include(q => q.Choices)
                .FirstOrDefaultAsync(q => q.Id == updatedQuestion.Id);

            if (existingQuestion == null) return;

            // 2. 親（Question）の情報を更新
            _context.Entry(existingQuestion).CurrentValues.SetValues(updatedQuestion);

            // 3. 【重要】新しく追加したい選択肢のデータを一度「別のリスト」にコピーして退避させる
            // これをしないと、削除処理中に元のリストが書き換わってエラーになります
            var newChoicesToAdd = updatedQuestion.Choices.Select(c => new Choice
            {
                Content = c.Content,
                IsCorrect = c.IsCorrect,
                QuestionId = updatedQuestion.Id
            }).ToList();

            // 4. 【重要】既存の選択肢を一度 .ToList() で「静的なリスト」にしてから削除
            // これにより「読み取りながら削除」によるエラーを防ぎます
            var oldChoices = existingQuestion.Choices.ToList();
            _context.Choices.RemoveRange(oldChoices);

            // 5. 退避しておいた新しい選択肢を一括追加
            _context.Choices.AddRange(newChoicesToAdd);

            // 6. 最後に一括で保存
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Update Error: {ex.Message}");
            throw;
        }
    }

    public async Task DeleteQuestionAsync(long id)
    {
        var question = await _context.Questions.FindAsync(id);
        if (question != null)
        {
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
        }
    }
}