using Dapper;
using System.Data;
using Domain.Dto._Base;

namespace Domain.DataAccess.Repository
{
    public interface IBaseRepository<T> : IDisposable
    {
        //Sync
        int Execute(string sql, object param = null, int? commandTimeout = default, CommandType? commandType = default);
        IEnumerable<T> Query(string sql, object param = null, bool buffered = true, int? commandTimeout = default, CommandType? commandType = default);
        int QueryExecute(string sql, object param, int? commandTimeout = default, CommandType? commandType = default);
        T QueryFirstOrDefault(string sql, object param = null, int? commandTimeout = default, CommandType? commandType = default);
        SqlMapper.GridReader QueryMultiple(string sql, object param = null, int? commandTimeout = default, CommandType? commandType = default);
        PagedResultDto<T> GetPaged(string sql, object param, int pageSize, int pageIndex, string orderField, int? commandTimeout);
        void Insert(T model);
        bool Update(T model);
        bool Delete(T model);

        //Async
        Task<int> ExecuteAsync(string sql, object param = null, int? commandTimeout = default, CommandType? commandType = default);
        Task<IEnumerable<T>> QueryAsync(string sql, object param = null, int? commandTimeout = default, CommandType? commandType = default);
        Task<int> QueryExecuteAsync(string sql, object param, int? commandTimeout = default, CommandType? commandType = default);
        Task<T> QueryFirstOrDefaultAsync(string sql, object param = null, int? commandTimeout = default, CommandType? commandType = default);
        Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null, int? commandTimeout = default, CommandType? commandType = default);
        Task<PagedResultDto<T>> GetPagedAsync(string sql, object param, int pageSize, int pageIndex, string orderField, int? commandTimeout);
        Task InsertAsync(T model);
        Task<bool> UpdateAsync(T model);
        Task<bool> DeleteAsync(T model);
    }
}
