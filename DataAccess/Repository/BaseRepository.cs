using Dapper;
using Domain.DataAccess.DataContext;
using Domain.DataAccess.Repository;
using System.Data;
using Dapper.FastCrud;
using Domain.Dto._Base;

namespace DataAccess.Repository
{
    public class BaseRepository<T> : IBaseRepository<T>
    {
        #region Constructor

        private readonly IConnectionKeeper _connectionKeeper;
        public BaseRepository(IConnectionKeeper connectionKeeper)
        {
            _connectionKeeper = connectionKeeper;
        }

        #endregion

        #region Sync

        public int Execute(string sql, object param, int? commandTimeout, CommandType? commandType)
        {
            using var db = _connectionKeeper.GetNewConnection();
            return db.Execute(sql, param, commandTimeout: commandTimeout, commandType: commandType);
        }

        public IEnumerable<T> Query(string sql, object param, bool buffered, int? commandTimeout, CommandType? commandType)
        {
            using var db = _connectionKeeper.GetNewConnection();
            return db.Query<T>(sql, param, buffered: buffered, commandTimeout: commandTimeout, commandType: commandType);
        }

        public int QueryExecute(string sql, object param, int? commandTimeout, CommandType? commandType)
        {
            using var db = _connectionKeeper.GetNewConnection();
            return db.QueryFirstOrDefault<int>(sql, param, commandTimeout: commandTimeout, commandType: commandType);
        }

        public T QueryFirstOrDefault(string sql, object param, int? commandTimeout, CommandType? commandType)
        {
            using var db = _connectionKeeper.GetNewConnection();
            return db.QueryFirstOrDefault<T>(sql, param, commandTimeout: commandTimeout, commandType: commandType);
        }

        public SqlMapper.GridReader QueryMultiple(string sql, object param, int? commandTimeout, CommandType? commandType)
        {
            using var db = _connectionKeeper.GetNewConnection();
            try
            {
                return db.QueryMultiple(sql, param, commandTimeout: commandTimeout, commandType: commandType);
            }
            catch
            {
                if (db.State != ConnectionState.Closed)
                    db.Close();

                throw;
            }
        }

        public PagedResultDto<T> GetPaged(string sql, object param, int pageSize, int pageIndex, string orderField, int? commandTimeout)
        {
            using var db = _connectionKeeper.GetNewConnection();

            try
            {
                var rowNumberFrom = pageIndex * pageSize;
                var rowNumberTo = (pageIndex + 1) * pageSize;

                var countQuery = @$"SELECT Count(*) FROM ({sql})";
                var selectQuery = $@"WITH CTE AS
                        (
                            SELECT t.*, ROW_NUMBER() over(ORDER BY t.{orderField}) RowNumber 
                            FROM ({sql}) t
                        )
                        SELECT * FROM CTE 
                        WHERE RowNumber >= {rowNumberFrom} AND RowNumber <= {rowNumberTo} ";

                var model = new PagedResultDto<T>
                {
                    Total = db.QueryFirst<int>(countQuery, param, commandTimeout: commandTimeout, commandType: CommandType.Text),
                    List = db.Query<T>(selectQuery, param, commandTimeout: commandTimeout, commandType: CommandType.Text)
                };

                return model;
            }

            catch
            {
                if (db.State != ConnectionState.Closed)
                    db.Close();

                throw;
            }
        }

        public void Insert(T model)
        {
            using var db = _connectionKeeper.GetNewConnection();
            db.Insert(model);
        }

        public bool Update(T model)
        {
            using var db = _connectionKeeper.GetNewConnection();
            return db.Update(model);
        }

        public bool Delete(T model)
        {
            using var db = _connectionKeeper.GetNewConnection();
            return db.Delete(model);
        }

        #endregion

        #region Async

        public async Task<int> ExecuteAsync(string sql, object param, int? commandTimeout, CommandType? commandType)
        {
            using var db = _connectionKeeper.GetNewConnection();
            return await db.ExecuteAsync(sql, param, commandTimeout: commandTimeout, commandType: commandType);
        }

        public async Task<IEnumerable<T>> QueryAsync(string sql, object param, int? commandTimeout, CommandType? commandType)
        {
            using var db = _connectionKeeper.GetNewConnection();

            try
            {
                return await db.QueryAsync<T>(sql, param, commandTimeout: commandTimeout, commandType: commandType);
            }
            catch
            {
                if (db.State != ConnectionState.Closed)
                    db.Close();

                throw;
            }
        }

        public async Task<int> QueryExecuteAsync(string sql, object param, int? commandTimeout, CommandType? commandType)
        {
            using var db = _connectionKeeper.GetNewConnection();

            try
            {
                return await db.QueryFirstOrDefaultAsync<int>(sql, param, commandTimeout: commandTimeout, commandType: commandType);
            }
            catch
            {
                if (db.State != ConnectionState.Closed)
                    db.Close();

                throw;
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync(string sql, object param, int? commandTimeout, CommandType? commandType)
        {
            using var db = _connectionKeeper.GetNewConnection();
            return await db.QueryFirstOrDefaultAsync<T>(sql, param, commandTimeout: commandTimeout, commandType: commandType);
        }

        public async Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param, int? commandTimeout, CommandType? commandType)
        {
            using var db = _connectionKeeper.GetNewConnection();

            try
            {
                return await db.QueryMultipleAsync(sql, param, commandTimeout: commandTimeout, commandType: commandType);
            }
            catch
            {
                if (db.State != ConnectionState.Closed)
                    db.Close();

                throw;
            }
        }

        public async Task<PagedResultDto<T>> GetPagedAsync(string sql, object param, int pageSize, int pageIndex, string orderField, int? commandTimeout)
        {
            using var db = _connectionKeeper.GetNewConnection();

            try
            {
                var rowNumberFrom = pageIndex * pageSize;
                var rowNumberTo = (pageIndex + 1) * pageSize;

                var countQuery = @$"SELECT Count(*) FROM ({sql})";
                var selectQuery = $@"WITH CTE AS
                        (
                            SELECT t.*, ROW_NUMBER() over(ORDER BY t.{orderField}) RowNumber 
                            FROM ({sql}) t
                        )
                        SELECT * FROM CTE 
                        WHERE RowNumber >= {rowNumberFrom} AND RowNumber <= {rowNumberTo} ";

                var model = new PagedResultDto<T>
                {
                    Total = await db.QueryFirstAsync<int>(countQuery, param, commandTimeout: commandTimeout, commandType: CommandType.Text),
                    List = await db.QueryAsync<T>(selectQuery, param, commandTimeout: commandTimeout, commandType: CommandType.Text)
                };

                //var gridReader = await db.QueryMultipleAsync(query, param, commandTimeout: commandTimeout, commandType: CommandType.Text);

                //if (!gridReader.IsConsumed)
                //    model.Total = await gridReader.ReadFirstAsync<int>();


                //if (!gridReader.IsConsumed)
                //    model.List = await gridReader.ReadAsync<T>();

                return model;
            }
            catch
            {
                if (db.State != ConnectionState.Closed)
                    db.Close();

                throw;
            }
        }

        public async Task InsertAsync(T model)
        {
            using var db = _connectionKeeper.GetNewConnection();
            await db.InsertAsync(model);
        }

        public async Task<bool> UpdateAsync(T model)
        {
            using var db = _connectionKeeper.GetNewConnection();
            return await db.UpdateAsync(model);
        }

        public async Task<bool> DeleteAsync(T model)
        {
            using var db = _connectionKeeper.GetNewConnection();
            return await db.DeleteAsync(model);
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            _connectionKeeper.Dispose();
        }

        #endregion
    }
}
