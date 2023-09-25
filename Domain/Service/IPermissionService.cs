using Domain.Dto.Permission;

namespace Domain.Service
{
    public interface IPermissionService
    {
        Task<IEnumerable<PermissionDto>> ListAsync();
    }
}
