using Domain.Dto.Permission;
using Domain.Dto.UserPermission;

namespace Domain.Service
{
    public interface IUserPermissionService
    {
        Task<IEnumerable<PermissionDto>> ListAsync(int userId);
        Task UpdateUserPermissionsAsync(UpdateUserPermissionsDto updateUserPermissionsDto);
    }
}
