using AutoMapper;
using FinalAssignmentBE.Dto;
using FinalAssignmentBE.Interfaces;
using FinalAssignmentBE.Models;

namespace FinalAssignmentBE.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    private readonly ILogger<GameService> _logger;
    private readonly IMapper _mapper;

    public GameService(IGameRepository gameRepository, ILogger<GameService> logger, IMapper mapper)
    {
        _gameRepository = gameRepository;
        _logger = logger;
        _mapper = mapper;
    }


    public async Task<List<BasicGameDto>> GetAllGames(GetGamesParamsDto? getGamesParams)
    {
        try
        {
            var games = await _gameRepository.GetAllGames(getGamesParams?.CreatedByUserId);

            return _mapper.Map<List<BasicGameDto>>(games);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _logger.LogError("Error Game Service GetAllGames:", e.Message);
            throw;
        }
    }

    public async Task<GameDto> GetGameById(long id)
    {
        try
        {
            var game = await _gameRepository.GetGameById(id);
            return _mapper.Map<GameDto>(game);
        }
        catch (Exception e)
        {
            _logger.LogError("Error Game Service GetGameById:", e.Message);
            throw;
        }
    }

    public async Task<GameDto> AddGame(AddGameDto addGameDto)
    {
        try
        {
            var game = _mapper.Map<Game>(addGameDto);
            var newGame = await _gameRepository.AddGame(game);
            return _mapper.Map<GameDto>(newGame);
        }
        catch (Exception e)
        {
            _logger.LogError("Error Game Service AddGame:", e.Message);
            throw;
        }
    }

    public async Task<GameDto> UpdateGame(long id, UpdateGameDto updateGameDto)
    {
        try
        {
            var foundGame = await _gameRepository.GetGameById(id);
            if (foundGame == null)
                throw new KeyNotFoundException("Game not found");
            if (!string.IsNullOrEmpty(updateGameDto.GameName))
                foundGame.GameName = updateGameDto.GameName;
            if (updateGameDto.TimeLimit != null)
                foundGame.TimeLimit = updateGameDto.TimeLimit;
            var updatedGame = await _gameRepository.UpdateGame(foundGame);
            return _mapper.Map<GameDto>(updatedGame);
        }
        catch (Exception e)
        {
            _logger.LogError("Error Game Service UpdateGame:", e.Message);
            throw;
        }
    }

    public async Task DeleteGame(long id)
    {
        try
        {
            await _gameRepository.DeleteGame(id);
        }
        catch (Exception e)
        {
            _logger.LogError("Error Game Service DeleteGame:", e.Message);
            throw;
        }
    }
}