using Dapper.FastCrud;
using DataAccess.Configuration.Register;
using Domain.DataAccess.Repository;
using Domain.DataModel;

namespace DataAccess.Repository
{
    public class UserPermissionRepository : IUserPermissionRepository
    {
        #region Constructor
        private readonly IBaseRepository<UserPermission> _userPermissionRepository;

        public UserPermissionRepository(IBaseRepository<UserPermission> userPermissionRepository)
        {
            _userPermissionRepository = userPermissionRepository;
        }
        #endregion

        #region Create

        public async Task<int> CreatePermissionsForUserAsync(int userId, IEnumerable<int> permissionIds)
        {
            var sql = @$"
                INSERT ALL
                {GetBulkInsertStatement(userId, permissionIds)}
                SELECT * FROM dual";
            return await _userPermissionRepository.ExecuteAsync(sql);
        }

        private static string GetBulkInsertStatement(int userId, IEnumerable<int> permissionIds)
        {
            return permissionIds.Select(p => @$"INTO {Sql.Table<UserPermission>().ToTableName()} (userId, permissionId) VALUES ({userId}, {p})").Aggregate((a, b) => $"{a}\n {b}");
        }
        #endregion

        #region Delete

        public async Task<int> DeletePermissionsForUserAsync(int userId, IEnumerable<int> permissionIds)
        {
            var sql = @$"
                DELETE FROM {Sql.Table<UserPermission>().ToTableName()}
                WHERE userId=:userId
                AND permissionId IN ({GetListConditions(permissionIds)})"; 
            return await _userPermissionRepository.ExecuteAsync(sql, new { userId });
        }

        private static string GetListConditions(IEnumerable<int> permissionIds)
        {
            return permissionIds.Select(p => @$"{p}").Aggregate((a, b) => $"{a}, {b}");
        }
        #endregion
    }
}
