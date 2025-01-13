using FinalAssignmentBE.Interfaces;
using FinalAssignmentBE.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalAssignmentBE.Repositories;

public class UserRepository : IUserRepository
{
    private readonly FinalAssignmentDbContext _context;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(FinalAssignmentDbContext context, ILogger<UserRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<User> AddUser(User user)
    {
        try
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        catch (Exception e)
        {
            _logger.LogError("Error AddUser Repository: ", e, e.Message);
            throw;
        }
    }

    public async Task<User> UpdateUser(User user)
    {
        try
        {
            var entry = _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Boolean> DeleteUser(User user)
    {
        try
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Error DeleteUser Repository: ", e, e.Message);
            
            throw;
        }
    }

    public async Task<List<User>?> GetUsers()
    {
        try
        {
            var users = await _context.Users.ToListAsync();

            return users;
        }
        catch (Exception e)
        {
            _logger.LogError("Error UserRepository: ", e, e.Message);
            throw;
        }
    }

    public async Task<User?> GetUserById(int id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }
        catch (Exception e)
        {
            _logger.LogError("Error UserRepository GetUserById: ", e, e.Message);
            throw;
        }
    }
}