namespace FinalAssignmentBE.Dto;

// Minimal Game Rule DTO
public class BasicGameRuleDto
{
    public int DivisibleNumber { get; set; }
    public string ReplacedWord { get; set; }
}

// Detailed Game Rule DTO including RuleId
public class GameRuleDto : BasicGameRuleDto
{
    public long RuleId { get; set; }
}

public class EditGameRuleDto
{
    public int? DivisibleNumber { get; set; }
    public string? ReplacedWord { get; set; }
}