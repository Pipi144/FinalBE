using FinalAssignmentBE.Interfaces;
using FinalAssignmentBE.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalAssignmentBE.Repositories;

public class GameRepository : IGameRepository
{
    private readonly FinalAssignmentDbContext _context;
    private readonly ILogger<GameRepository> _logger;

    public GameRepository(FinalAssignmentDbContext context, ILogger<GameRepository> logger)
    {
        _context = context;
        _logger = logger;
    }


    public async Task<IEnumerable<Game>> GetAllGames(long? userId)
    {
        try
        {
            if (userId != null && userId < 0)
                throw new ArgumentException("User Id can't be negative.");
            var gamesQuery = _context.Games.AsQueryable();
            if (userId != null)
                gamesQuery = gamesQuery.Where(q => q.CreatedByUserId == userId)
                    .OrderByDescending(q => q.CreatedAt);
            return await gamesQuery.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("Error GameRepository GetAllGames:", e.Message);
            throw;
        }
    }

    public async Task<Game?> GetGameById(long id)
    {
        try
        {
            if (id < 0)
                throw new ArgumentException("Game Id can't be negative.");
            var foundGame = await _context.Games.FirstOrDefaultAsync(g => g.GameId == id);
            if (foundGame == null)
                throw new KeyNotFoundException($"Game Id {id} not found.");
            return foundGame;
        }
        catch (Exception e)
        {
            _logger.LogError("Error GameRepository GetGameById:", e.Message);
            throw;
        }
    }

    public async Task<Game> AddGame(Game game)
    {
        try
        {
            var newGame = await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
            return newGame.Entity;
        }
        catch (Exception e)
        {
            _logger.LogError("Error GameRepository.AddGame:", e.Message);
            throw;
        }
    }

    public async Task<Game> UpdateGame(Game game)
    {
        try
        {
            _context.Games.Update(game);
            await _context.SaveChangesAsync();
            return game;
        }
        catch (Exception e)
        {
            _logger.LogError("Error GameRepository.UpdateGame:", e.Message);
            throw;
        }
    }

    public async Task DeleteGame(long id)
    {
        try
        {
            var gameToDelete = await _context.Games.FirstOrDefaultAsync(g => g.GameId == id);
            if (gameToDelete == null) throw new KeyNotFoundException($"Game Id {id} not found.");
            _context.Games.Remove(gameToDelete);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("Error GameRepository.DeleteGame:", e.Message);
            throw;
        }
    }
}