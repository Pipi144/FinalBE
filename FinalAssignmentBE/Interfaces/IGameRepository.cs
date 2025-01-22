using FinalAssignmentBE.Models;

namespace FinalAssignmentBE.Interfaces;

public interface IGameRepository
{
    public Task<IEnumerable<Game>> GetAllGames(long? userId);
    public Task<Game?> GetGameById(long id);
    public Task<Game> AddGame(Game game);
    public Task<Game> UpdateGame(Game game);
    public Task DeleteGame(long id);
    
    
}