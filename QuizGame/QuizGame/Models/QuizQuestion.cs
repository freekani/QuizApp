using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace QuizGame.Models;

public class QuizQuestion
{
    public long Id { get; set; }
    public long QuizId { get; set; }
    public long QuestionId { get; set; }
    public int? Position { get; set; }
    [Column(TypeName = "jsonb")] public string LocalMetadata { get; set; } = "{}";
    public long? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // ナビゲーションプロパティ
    public Quiz Quiz { get; set; } = null!;
    public Question Question { get; set; } = null!;
}