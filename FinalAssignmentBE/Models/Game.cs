using System.ComponentModel.DataAnnotations;

namespace FinalAssignmentBE.Models;

public class Game
{
    [Key]
    public long GameId { get; set; } 
    public string GameName { get; set; }
    public int? TimeLimit { get; set; } = null;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public long CreatedByUserId { get; set; }
    public User User { get; set; }
    
    public List<GameRule> GameRules { get; set; } = new List<GameRule>();
}