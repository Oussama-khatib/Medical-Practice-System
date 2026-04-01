using App.Application;
using App.core;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using WebApp.API.Service;

namespace WebApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.ListUsers();
            return Ok(users);  
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           var createdUser= await _userService.CreateUser(user);
            if (createdUser == null)
            {
                return BadRequest();
            }

            return Ok();  
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            user.UserId = id;
            var updatedUser = await _userService.UpdateUser(user);
            if (updatedUser == null)
            {
                return NotFound();  // Return 404 if the user to update does not exist
            }

            return Ok(updatedUser);  // Return 200 OK with the updated user
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
             await _userService.RemoveUser(id);
            return Ok("User deleted successfully.");  
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] App.core.LoginRequest login)
        {
            var users = await _userService.ListUsers();
            var user = users.FirstOrDefault(u =>
                u.Email == login.Email &&
                u.Password == login.Password);
            UserTypeService userTypeService = new UserTypeService();
            var userType = await userTypeService.GetUserTypeById(user.UserTypeId);


            if (user == null)
                return Unauthorized("Invalid credentials");

            // ✅ Generate JWT token for this user
            var token = TokenService.GenerateTokenUser(user);

            user.userType = userType;

            return Ok(new
            {
                message = "Login successful",
                token,
                user = new { user.UserId, user.FirstName,user.LastName,user.Password, user.Email,user.UserTypeId, user.userType }
            });
        }
    }

    }

