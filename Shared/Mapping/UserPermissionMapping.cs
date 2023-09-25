using Domain.Dto.UserPermission;
using Shared.Model.UserPermission;

namespace Shared.Mapping
{
    public static class UserPermissionMapping
    {
        #region EmployeeViewModel => EmployeeDto

        public static UpdateUserPermissionsDto ToDto(this UpdateUserPermissionsRequest model)
        {
            return new UpdateUserPermissionsDto
            {
                PermissionIds = model.PermissionIds,
                Id = model.userId
            };
        }

        #endregion
    }
}
