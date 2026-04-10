using QuizGame.Models;

namespace QuizGame.Services;

public interface IQuizService
{
    Task<List<Question>> GetQuizzesAsync();
    Task<Question?> GetQuestionByIdAsync(long id); 
    Task AddQuizAsync(Question question);

    Task UpdateQuestionAsync(Question updatedQuestion); 
    Task DeleteQuestionAsync(long id);
}