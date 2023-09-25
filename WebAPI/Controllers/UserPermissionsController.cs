using Domain.Service;
using Microsoft.AspNetCore.Mvc;
using Shared.Mapping;
using Shared.Model.UserPermission;

namespace WebAPI.Controllers
{
    public class UserPermissionsController : ApiControllerBase
    {
        private readonly IUserPermissionService _userPermissionService;

        public UserPermissionsController(IUserPermissionService userPermissionService)
        {
            _userPermissionService = userPermissionService;
        }

        [HttpGet("/Users/{id}/permissions")]
        public async Task<IActionResult> ListByUser([FromRoute] int id)
        {
            return Ok(await _userPermissionService.ListAsync(id));
        }

        [HttpPut("/Users/{id}/permissions")]
        public async Task<IActionResult> UpdatePermissions([FromRoute] UpdateUserPermissionsRequest request)
        {
            await _userPermissionService.UpdateUserPermissionsAsync(request.ToDto());
            return Ok();
        }
    }
}
