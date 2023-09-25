using Dapper.FastCrud;
using DataAccess.Configuration.Register;
using Domain.DataAccess.Repository;
using Domain.DataModel;

namespace DataAccess.Repository
{
    public class PermissionRepository : IPermissionRepository
    {
        #region Constructor
        private readonly IBaseRepository<Permission> _repository;
        public PermissionRepository(IBaseRepository<Permission> repository)
        {
            _repository = repository;
        }
        #endregion

        #region List
        public async Task<IEnumerable<Permission>> ListAsync()
        {
            var sql = $"SELECT * FROM {Sql.Table<Permission>().ToTableName()} ";
            return await _repository.QueryAsync(sql);
        }

        public async Task<IEnumerable<Permission>> ListByUserIdAsync(int id)
        {
            var sql = @$"
                SELECT p.* FROM {Sql.Table<Permission>().ToTableName()} p 
                JOIN {Sql.Table<UserPermission>().ToTableName()} up ON p.id = up.permissionId
                WHERE up.userId = :id
                ";
            return await _repository.QueryAsync(sql, new {id});
        }
        #endregion
    }
}
