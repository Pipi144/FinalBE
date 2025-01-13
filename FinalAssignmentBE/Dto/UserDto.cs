using FinalAssignmentBE.Models;

namespace FinalAssignmentBE.Dto;

public class UserDto
{
    public int UserId {get; set;}
    public string Username {get; set;}
}

public class AddUserDto
{
    public string Username {get; set;}
    public string Password {get; set;}
}

public class UpdateUserDto
{
    public string? Username {get; set;}
    public string? Password {get; set;}
}