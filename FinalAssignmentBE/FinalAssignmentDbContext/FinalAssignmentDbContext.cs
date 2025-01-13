using FinalBE.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalBE.Context;

public class FinalAssignmentDbContext : DbContext
{
    public FinalAssignmentDbContext(DbContextOptions<FinalAssignmentDbContext> options) : base(options) { }
    
    
    public DbSet<User> Users { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<GameRule> GameRules { get; set; }
    public DbSet<GameAttempt> GameAttempts { get; set; }
    
}