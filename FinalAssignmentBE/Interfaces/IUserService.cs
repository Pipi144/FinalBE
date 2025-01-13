using FinalAssignmentBE.Dto;

namespace FinalAssignmentBE.Interfaces;

public interface IUserService
{
    public Task<UserDto> AddUser(AddUserDto addUserDto);   
    public Task<UserDto?> GetUserById(int id);
    public Task<List<UserDto>> GetUsers();
    
    public Task<bool> DeleteUser(int id);
    public Task<UserDto> UpdateUser(int id, UpdateUserDto payload);
}