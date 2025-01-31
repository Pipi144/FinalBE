using AutoMapper;
using FinalAssignmentBE.Dto;
using FinalAssignmentBE.Interfaces;
using FinalAssignmentBE.Models;

namespace FinalAssignmentBE.Services;

public class GameAttemptService: IGameAttemptService
{
    private readonly ILogger<GameAttemptService> _logger;
    private readonly IGameAttemptRepository _gameAttemptRepository;
    private readonly IMapper _mapper;

    public GameAttemptService(ILogger<GameAttemptService> logger, IGameAttemptRepository gameAttemptRepository, IMapper mapper)
    {
        _logger = logger;
        _gameAttemptRepository = gameAttemptRepository;
        _mapper = mapper;
    }
    
    
    public async Task<GameAttemptDto> CreateGameAttempt(AddGameAttemptDto payload)
    {
        try
        {
            var newGameAttempt = _mapper.Map<GameAttempt>(payload);
            var addResult = await _gameAttemptRepository.AddGameAttempt(newGameAttempt);
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<GameAttemptDto> GetGameAttempt(int id)
    {
        throw new NotImplementedException();
    }
}