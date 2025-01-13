using FinalAssignmentBE.Dto;
using FinalAssignmentBE.Interfaces;
using FinalAssignmentBE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalAssignmentBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            try
            {
                var users =await _userService.GetUsers();
                return Ok(users);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

       //Patch api
       [HttpPatch("{id}")]
       public async Task<ActionResult<UserDto>> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
       {
           try
           {
               var updatedResult = _userService.UpdateUser(id, updateUserDto);
               return Ok(updatedResult);
           }
           catch (Exception e)
           {
               Console.WriteLine(e);
               throw;
           }
       }
        
       //Delete api
       [HttpDelete("{id}")]
       public async Task<ActionResult> DeleteUser(int id)
       {
           try
           {
               await _userService.DeleteUser(id);
               return NoContent();
           }
           catch (Exception e)
           {
               Console.WriteLine(e);
               throw;
           }
       }
       
       // Get user with id
       [HttpGet("{id}")]
       public async Task<ActionResult<UserDto>> GetUser(int id)
       {
           try
           {
               var user = await _userService.GetUserById(id);
               return Ok(user);
           }
           catch (Exception e)
           {
               Console.WriteLine(e);
               throw;
           }
       }
       
       
       //Add user
       [HttpPost]
       public async Task<ActionResult<UserDto>> AddUser([FromBody] AddUserDto userDto)
       {
           try
           {
               var addedUser = await _userService.AddUser(userDto);
               return CreatedAtAction(nameof(GetUser), new {id = addedUser.UserId}, addedUser);
           }
           catch (Exception e)
           {
               Console.WriteLine(e);
               throw;
           }
       }
    }
}
