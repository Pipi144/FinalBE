using Microsoft.AspNetCore.SignalR;

namespace FinalBE.Models;

public class Game
{
    public int GameId { get; set; } 
    public string GameName { get; set; }
    public int TimeLimit { get; set; }
    public int CreatedByUserId { get; set; }
    public User User { get; set; }
}