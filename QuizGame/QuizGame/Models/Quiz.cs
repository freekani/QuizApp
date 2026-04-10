using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace QuizGame.Models;

// --- クイズ全体 (quizzes) ---
public class Quiz
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    [Column(TypeName = "jsonb")] public string Metadata { get; set; } = "{}";
    public long? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // リレーション
    public List<QuizQuestion> QuizQuestions { get; set; } = new();
}

