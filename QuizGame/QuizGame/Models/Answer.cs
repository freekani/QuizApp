using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace QuizGame.Models;

public class Answer
{
    public long Id { get; set; }
    public long QuestionId { get; set; }
    public string Content { get; set; } = string.Empty;
    public string NormalizedContent { get; set; } = string.Empty;
    public string MatchType { get; set; } = "exact"; // exact, contains, regex
    public decimal Points { get; set; } = 1;
}