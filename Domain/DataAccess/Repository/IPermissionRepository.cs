using Domain.DataModel;

namespace Domain.DataAccess.Repository
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> ListAsync();
        Task<IEnumerable<Permission>> ListByUserIdAsync(int id);
    }
}
