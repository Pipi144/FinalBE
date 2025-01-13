using System.ComponentModel.DataAnnotations;

namespace FinalAssignmentBE.Models;

public class GameAttempt
{
    [Key]
    public int AttemptId { get; set; }
    public int Score { get; set; }
    public int GameId { get; set; }
    public Game Game { get; set; }
    public int AttemptByUserId { get; set; }
    public User User { get; set; }
}