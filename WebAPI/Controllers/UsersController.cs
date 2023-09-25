using Domain.Service;
using Microsoft.AspNetCore.Mvc;
using Shared.Mapping;
using Shared.Model.User;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    public class UsersController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            return Ok(await _userService.ListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserRequest request)
        {
            await _userService.CreateAsync(request.ToDto());
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _userService.DeleteAsync(id);
            return Ok();
        }
    }
}
