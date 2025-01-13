using System.ComponentModel.DataAnnotations;

namespace FinalAssignmentBE.Models;

public class Game
{
    [Key]
    public int GameId { get; set; } 
    public string GameName { get; set; }
    public int TimeLimit { get; set; }
    public int CreatedByUserId { get; set; }
    public User User { get; set; }
}