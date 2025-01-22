using System.ComponentModel.DataAnnotations;

namespace FinalAssignmentBE.Models;

public class GameAttempt
{
    [Key]
    public long AttemptId { get; set; }
    public int Score { get; set; }
    public long GameId { get; set; }
    public DateTime AttemptedDate { get; set; } = DateTime.UtcNow;
    public Game Game { get; set; }
    public long AttemptByUserId { get; set; }
    public User User { get; set; }
}