namespace FinalBE.Models;

public class GameRule
{
    public int RuleId { get; set; }
    public int DivisibleNumber { get; set; }
    public string ReplacedWord { get; set; }
    public int GameId { get; set; }
    public Game Game { get; set; }
}