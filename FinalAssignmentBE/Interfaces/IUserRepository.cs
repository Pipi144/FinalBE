using FinalAssignmentBE.Models;

namespace FinalAssignmentBE.Interfaces;

public interface IUserRepository
{
    public Task<User> AddUser(User user);
    public Task<User> UpdateUser(User user);
    public Task DeleteUser(long userId);
    public Task<List<User>?> GetUsers();
    public Task<User?> GetUserById(long id);
}