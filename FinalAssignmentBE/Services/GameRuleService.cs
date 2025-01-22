using AutoMapper;
using FinalAssignmentBE.Dto;
using FinalAssignmentBE.Interfaces;
using FinalAssignmentBE.Models;

namespace FinalAssignmentBE.Services;

public class GameRuleService:IGameRuleService
{
    private readonly IGameRuleRepository _gameRuleRepository;
    private readonly ILogger<GameRuleService> _logger;
    private IMapper _mapper;
    public GameRuleService(IGameRuleRepository gameRuleRepository, ILogger<GameRuleService> logger, IMapper mapper)
    {
        _gameRuleRepository = gameRuleRepository;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<GameRuleDto> AddGameRule(BasicGameRuleDto gameRule)
    {
        try
        {
            var addedGameRule = _mapper.Map<GameRule>(gameRule);
            var result = await _gameRuleRepository.AddGameRule(addedGameRule);
            return _mapper.Map<GameRuleDto>(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<GameRuleDto> EditGameRule(EditGameRuleDto editPayloadDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteGameRuleById(long id)
    {
        throw new NotImplementedException();
    }
}