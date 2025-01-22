using FinalAssignmentBE.Dto;

namespace FinalAssignmentBE.Interfaces;

public interface IGameRuleService
{
    public Task<GameRuleDto> AddGameRule(BasicGameRuleDto gameRule);
    public Task<GameRuleDto> EditGameRule(EditGameRuleDto editPayloadDto);
    public Task DeleteGameRuleById(long id);
}