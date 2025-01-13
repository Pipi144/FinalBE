using FinalAssignmentBE.Controllers;
using FinalAssignmentBE.Dto;
using FinalAssignmentBE.Interfaces;
using FinalAssignmentBE.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FinalAssignmentBETest;

[TestFixture]
public class UserControllerTest
{
    private Mock<IUserService> _mockUserService;
    private UserController _userController;

    [SetUp]
    public void Setup()
    {
        _mockUserService = new Mock<IUserService>();
        _userController = new UserController(_mockUserService.Object);
    }

    [Test]
    public async Task GetUsers_ReturnsOkResult_WithListOfUsers()
    {
        // Arrange
        var mockUsers = new List<UserDto>
        {
            new UserDto { UserId = 1, Username = "John Doe" },
            new UserDto { UserId = 1, Username = "Peter" }
        };

        _mockUserService.Setup(service => service.GetUsers()).ReturnsAsync(mockUsers);


        // Act
        var result = await _userController.GetUsers();

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That((okResult?.Value), Is.EqualTo(mockUsers));
    }

    [Test]
    public void GetUsers_ThrowsException_ReturnsInternalServerError()
    {
        // Arrange
        _mockUserService.Setup(service => service.GetUsers())
            .Throws(new Exception("Database connection error"));

        // Act & Assert
        Assert.ThrowsAsync<Exception>(async () => await _userController.GetUsers());
    }

    [Test]
    public async Task UpdateUser_ShouldReturnOkWithUpdatedUser()
    {
        // Arrange
        var userId = 1;
        var updateUserDto = new UpdateUserDto { Username = "UpdatedUser", Password = "NewPassword" };
        var updatedUser = new UserDto { UserId = userId, Username = "UpdatedUser" };

        _mockUserService.Setup(s => s.UpdateUser(userId, updateUserDto)).ReturnsAsync(updatedUser);

        // Act
        var result = await _userController.UpdateUser(userId, updateUserDto);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>(), "Result was not returned");
        var okResult = result.Result as OkObjectResult;
        Assert.That((okResult.Value as Task<UserDto>).Result, Is.EqualTo(updatedUser));
    }


    [Test]
    public void UpdateUser_ThrowsException_ReturnsInternalServerError()
    {
        // Arrange
        int userId = 1;
        var updateUserDto = new UpdateUserDto { Username = "UpdatedUser", Password = "NewPassword" };
        _mockUserService.Setup(service => service.UpdateUser(userId, updateUserDto))
            .Throws(new Exception("Database connection error"));

        // Act & Assert
        Assert.ThrowsAsync<Exception>(async () => await _userController.UpdateUser(userId, updateUserDto));
    }


    [Test]
    public async Task DeleteUser_ShouldReturnNoContent()
    {
        // Arrange
        var userId = 1;
        _mockUserService.Setup(s => s.DeleteUser(userId)).ReturnsAsync(true);

        // Act
        var result = await _userController.DeleteUser(userId);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }


    [Test]
    public void DeleteUser_ThrowsException_ReturnsInternalServerError()
    {
        // Arrange
        var userId = 1;
        _mockUserService.Setup(service => service.DeleteUser(userId))
            .Throws(new Exception("Database connection error"));

        // Act & Assert
        Assert.ThrowsAsync<Exception>(async () => await _userController.DeleteUser(userId));
    }


    [Test]
    public async Task RegisterUser_ShouldReturnOkResult_WithRegisteredUser()
    {
        //Arrange
        var addedUserDto = new AddUserDto { Username = "AddedUser", Password = "NewPassword" };
        var addedUser = new UserDto { UserId = 1, Username = "AddedUser" };
        _mockUserService.Setup(service=> service.AddUser(addedUserDto)).ReturnsAsync(addedUser);
        
        //Act
        var result = await _userController.AddUser(addedUserDto);
        
        //Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var okResult = result.Result as CreatedAtActionResult;
        Assert.That((okResult?.ActionName), Is.EqualTo("GetUser"));
        Assert.That(okResult.Value, Is.EqualTo(addedUser));
    }

    [Test]
    public void RegisterUser_ThrowsException_ReturnsInternalServerError()
    {
        //Arrange
        var addedUserDto = new AddUserDto { Username = "AddedUser", Password = "NewPassword" };
        _mockUserService.Setup(service => service.AddUser(addedUserDto))
            .Throws(new Exception("Database connection error"));

        // Act & Assert
        Assert.ThrowsAsync<Exception>(async () => await _userController.AddUser(addedUserDto));
    }
}