using App.Application;
using App.core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeService _userTypeService;
        public UserTypeController(IUserTypeService userTypeService) 
        {
            _userTypeService = userTypeService;
        }
        // GET: api/UserType
        [HttpGet]
        public async Task<IActionResult> GetAllUserType()
        {
            var userTypes = await _userTypeService.ListUserTypes();
            return Ok(userTypes);
        }

        // POST: api/UserType
        [HttpPost]
        public async Task<IActionResult> CreateUserType([FromBody] UserType userType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _userTypeService.CreateUserType(userType);
            return Ok();
        }

        // PUT: api/UserType/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserType(int id, [FromBody] UserType userType)
        {
            if (id != userType.UserTypeId)
            {
                return BadRequest();
            }
            await _userTypeService.UpdateUserType(userType);
            return NoContent();
        }

        // DELETE: api/UserType/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserType(int id)
        {
            await _userTypeService.RemoveUserType(id);
            return NoContent();
        }
    }
}
