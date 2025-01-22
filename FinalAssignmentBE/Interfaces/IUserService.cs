using FinalAssignmentBE.Dto;

namespace FinalAssignmentBE.Interfaces;

public interface IUserService
{
    public Task<UserDto> AddUser(AddUserDto addUserDto);   
    public Task<UserDto?> GetUserById(long id);
    public Task<List<UserDto>> GetUsers();
    
    public Task DeleteUser(long id);
    public Task<UserDto> UpdateUser(long id, UpdateUserDto payload);
}