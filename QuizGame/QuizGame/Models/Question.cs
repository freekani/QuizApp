using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace QuizGame.Models;
public class Question
{
    public long Id { get; set; }
    public string Type { get; set; } = "multiple_choice"; // multiple_choice, text, etc.
    public string Content { get; set; } = string.Empty;
    [Column(TypeName = "jsonb")] public string Metadata { get; set; } = "{}";
    public bool IsActive { get; set; } = true;
    public long? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // リレーション
    public List<Choice> Choices { get; set; } = new();
    public List<Answer> Answers { get; set; } = new();
    public List<QuizQuestion> QuizQuestions { get; set; } = new();
}