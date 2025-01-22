using AutoMapper;
using FinalAssignmentBE.Dto;
using FinalAssignmentBE.Interfaces;
using FinalAssignmentBE.Models;
using FinalAssignmentBE.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Moq;

namespace FinalAssignmentBETest;

[TestFixture]
public class UserServiceTest
{
    private UserService _userService;
    private Mock<IUserRepository> _userRepositoryMock;
    private Mock<ILogger<UserService>> _loggerMock;
    private Mock<IMapper> _mapperMock;

    [SetUp]
    public void Setup()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _loggerMock = new Mock<ILogger<UserService>>();
        _mapperMock = new Mock<IMapper>();
        _userService = new UserService(_userRepositoryMock.Object, _loggerMock.Object, _mapperMock.Object);
    }

    [Test]
    public void AddUserAsync_InvalidUser_ThrowsException()
    {
        // Arrange
        var addUserDto = new AddUserDto
        {
            Username = "Pipi",
            Password = "pipi"
        };

        var mockUser = new User
        {
            Username = addUserDto.Username,
            Password = addUserDto.Password,
            UserId = 4
        };
        // Mock repository behavior for duplicate username
        _userRepositoryMock.Setup(repo => repo.AddUser(It.Is<User>(u => u.Username == addUserDto.Username)))
            .ThrowsAsync(new ArgumentException($"Username {addUserDto.Username} is already taken"));
        _mapperMock.Setup(mapper => mapper.Map<User>(addUserDto)).Returns(mockUser);

        // Act & Assert
        var exception = Assert.ThrowsAsync<ArgumentException>(async () => await _userService.AddUser(addUserDto));

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception.Message, Is.EqualTo($"Username {addUserDto.Username} is already taken"));
    }

    [Test]
    public async Task AddUserAsync_ValidUser_Success()
    {
        // Arrange
        var addUserDto = new AddUserDto
        {
            Username = "Pipi1234",
            Password = "pipi"
        };

        var mockUser = new User
        {
            Username = addUserDto.Username,
            Password = addUserDto.Password,
            UserId = 4
        };

        var expectedUserDto = new UserDto
        {
            UserId = mockUser.UserId,
            Username = mockUser.Username
        };

        _userRepositoryMock.Setup(repo => repo.AddUser(It.Is<User>(u =>
                u.Username == addUserDto.Username && u.Password == addUserDto.Password)))
            .ReturnsAsync(mockUser);

        _mapperMock.Setup(mapper => mapper.Map<User>(addUserDto)).Returns(mockUser);
        _mapperMock.Setup(mapper => mapper.Map<UserDto>(mockUser)).Returns(expectedUserDto);

        // Act
        var result = await _userService.AddUser(addUserDto);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Username, Is.EqualTo(expectedUserDto.Username));
        Assert.That(result.UserId, Is.EqualTo(expectedUserDto.UserId));

        // Verify repository and mapper calls
        _userRepositoryMock.Verify(repo => repo.AddUser(It.IsAny<User>()), Times.Once);
        _mapperMock.Verify(mapper => mapper.Map<User>(addUserDto), Times.Once);
        _mapperMock.Verify(mapper => mapper.Map<UserDto>(mockUser), Times.Once);
    }

    [Test]
    public async Task GetUserById_Success_ReturnsMatchingUser()
    {
        //Arrange
        var userId = 1;

        var mockUser = new User
        {
            Username = "Pipi",
            Password = "1234",
            UserId = 1
        };

        var expectedUserDto = new UserDto
        {
            UserId = mockUser.UserId,
            Username = mockUser.Username
        };
        _userRepositoryMock.Setup(s => s.GetUserById(userId)).ReturnsAsync(mockUser);
        _mapperMock.Setup(s => s.Map<UserDto>(mockUser)).Returns(expectedUserDto);

        //Act
        var result = await _userService.GetUserById(userId);

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Username, Is.EqualTo(expectedUserDto.Username));
        Assert.That(result.UserId, Is.EqualTo(expectedUserDto.UserId));
    }

    [Test]
    public void GetUserById_NotExistingUser_ThrowNotFoundException()
    {
        // Arrange
        var userId = 999;
        _userRepositoryMock.Setup(s => s.GetUserById(userId))
            .ThrowsAsync(new KeyNotFoundException("User does not exist"));
        // Act and Assert
        var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _userService.GetUserById(userId));

        Assert.That(exception.Message, Is.EqualTo("User does not exist"));
    }

    [Test]
    public async Task GetUsers_Success_ReturnsAllUsers()
    {
        //Arrange
        var users = new List<User>()
        {
            new User()
            {
                Username = "Pipi",
                Password = "1234",
                UserId = 1
            },
            new User()
            {
                Username = "Pipi",
                Password = "1234",
                UserId = 2
            }
        };
        var expectedUsers = new List<UserDto>()
        {
            new UserDto()
            {
                UserId = 1,
                Username = "Pipi",
            },
            new UserDto()
            {
                UserId = 2,
                Username = "Pipi",
            }
        };
        _userRepositoryMock.Setup(s => s.GetUsers()).ReturnsAsync(users);
        _mapperMock.Setup(s => s.Map<List<UserDto>>(users)).Returns(expectedUsers);

        //Act
        var result = await _userService.GetUsers();

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(users.Count));
        Assert.That(result[0].Username, Is.EqualTo(expectedUsers[0].Username));
        Assert.That(result[0].UserId, Is.EqualTo(expectedUsers[0].UserId));
        Assert.That(result[1].Username, Is.EqualTo(expectedUsers[1].Username));
        Assert.That(result[1].UserId, Is.EqualTo(expectedUsers[1].UserId));
    }

    [Test]
    public async Task DeleteUserById_Success_RemovesUser()
    {
        //Arrange
        var userId = 1;

        _userRepositoryMock.Setup(s => s.DeleteUser(userId)).Returns(Task.CompletedTask);

        // Act
        await _userService.DeleteUser(userId);

        //Assert
        _userRepositoryMock.Verify(repo => repo.DeleteUser(userId), Times.Once);
    }

    [Test]
    public void DeleteUserById_NotExistingUser_ThrowNotFoundException()
    {
        //Assert
        var userId = 999;
        _userRepositoryMock.Setup(s => s.DeleteUser(userId)).ThrowsAsync(new KeyNotFoundException("User does not exist"));
        
        //Act
        var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _userService.DeleteUser(userId));
        
        //Assert
        Assert.That(exception.Message, Is.EqualTo("User does not exist"));
        _userRepositoryMock.Verify(repo => repo.DeleteUser(userId), Times.Once);
    }

    [Test]
    public async Task UpdateUser_Success_ReturnsUpdatedUser()
    {
        // Arrange
        var userId = 1;
        var payload = new UpdateUserDto()
        {
            Username = "UpdatedPipi",
            Password = "Updatedpassword",
        };
        var mockUser = new User()
        {
            Username = "Pipi",
            Password = "1234",
            UserId = userId
        };
        var expectedUserDto = new UserDto()
        {
            UserId = mockUser.UserId,
            Username = mockUser.Username,
        };
        _userRepositoryMock.Setup(s => s.GetUserById(userId)).ReturnsAsync(mockUser);
        _userRepositoryMock.Setup(s => s.UpdateUser(It.Is<User>(u=>u.Username == mockUser.Username && u.Password==mockUser.Password))).ReturnsAsync(mockUser);
        _mapperMock.Setup(s => s.Map<UserDto>(mockUser)).Returns(expectedUserDto);
        
        //Act
        var result = await _userService.UpdateUser(userId, payload);
        
        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Username, Is.EqualTo(expectedUserDto.Username));
        Assert.That(result.UserId, Is.EqualTo(expectedUserDto.UserId));
        _userRepositoryMock.Verify(repo => repo.UpdateUser(It.IsAny<User>()), Times.Once);
        _userRepositoryMock.Verify(repo => repo.GetUserById(userId), Times.Once);
    }


    [Test]
    public void UpdateUser_NotExistingUser_ThrowNotFoundException()
    {
        //Arrange
        var userId = 999;
        var payload = new UpdateUserDto()
        {
            Username = "UpdatedPipi",
        };
        _userRepositoryMock.Setup(s => s.GetUserById(userId)).ThrowsAsync(new KeyNotFoundException("User does not exist"));
        
        //Act
        var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _userService.UpdateUser(userId, payload));
 
        //Assert
        Assert.That(exception.Message, Is.EqualTo("User does not exist"));
    }
}