namespace QuizGame.Data;
using Microsoft.EntityFrameworkCore;
using QuizGame.Models;

public class QuizDbContext : DbContext
{
    public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options) { }

    // QuizItem を消して、以下を追加
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Choice> Choices { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<QuizQuestion> QuizQuestions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<QuizQuestion>()
            .HasIndex(qq => new { qq.QuizId, qq.QuestionId })
            .IsUnique();
        
        base.OnModelCreating(modelBuilder);
    }
}