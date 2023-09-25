using Microsoft.AspNetCore.Mvc;

namespace Shared.Model.UserPermission
{
    public class UpdateUserPermissionsRequest
    {
        [FromRoute(Name = "id")] public int userId { get; set; }
        [FromBody] public IEnumerable<int> PermissionIds { get; set; }
    }
}
