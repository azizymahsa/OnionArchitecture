namespace DataAccess.Repository
{
    public interface IUserPermissionRepository
    {

        Task<int> CreatePermissionsForUserAsync(int userId, IEnumerable<int> permissionIds);
        Task<int> DeletePermissionsForUserAsync(int userId, IEnumerable<int> permissionIds);
    }
}