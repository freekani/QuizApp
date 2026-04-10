using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace QuizGame.Models;

public class Choice
{
    public long Id { get; set; }
    public long QuestionId { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public decimal Points { get; set; } = 0;
    public int? Position { get; set; }
    [Column(TypeName = "jsonb")] public string Metadata { get; set; } = "{}";
}