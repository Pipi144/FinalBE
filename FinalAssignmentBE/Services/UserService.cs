using AutoMapper;
using FinalAssignmentBE.Dto;
using FinalAssignmentBE.Interfaces;
using FinalAssignmentBE.Models;

namespace FinalAssignmentBE.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, ILogger<UserService> logger, IMapper mapper)
    {
        _userRepository = userRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<UserDto> AddUser(AddUserDto addUserDto)
    {
        try
        {
            var user = _mapper.Map<User>(addUserDto);
            var result = await _userRepository.AddUser(user);

            return _mapper.Map<UserDto>(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<UserDto?> GetUserById(int id)
    {
        try
        {
            var user = await _userRepository.GetUserById(id);
            return _mapper.Map<UserDto>(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<UserDto>> GetUsers()
    {
        try
        {
            var users = await _userRepository.GetUsers();
            return _mapper.Map<List<UserDto>>(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DeleteUser(int id)
    {
        try
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            await _userRepository.DeleteUser(user);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Delete user service:", e.Message);
            throw;
        }
    }

    public async Task<UserDto> UpdateUser(int id, UpdateUserDto payload)
    {
        try
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            if (!string.IsNullOrEmpty(payload.Username))
                user.Username = payload.Username;
            if (!string.IsNullOrEmpty(payload.Password))
                user.Password = payload.Password;
            var updatedUser = await _userRepository.UpdateUser(user);
            return _mapper.Map<UserDto>(updatedUser);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}