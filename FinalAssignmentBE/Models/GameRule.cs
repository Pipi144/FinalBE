using System.ComponentModel.DataAnnotations;

namespace FinalAssignmentBE.Models;

public class GameRule
{
    [Key]
    public int RuleId { get; set; }
    public int DivisibleNumber { get; set; }
    public string ReplacedWord { get; set; }
    public int GameId { get; set; }
    public Game Game { get; set; }
}