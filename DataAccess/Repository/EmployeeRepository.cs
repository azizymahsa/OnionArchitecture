using Dapper.FastCrud;
using DataAccess.Configuration.Register;
using DataAccess.Helper;
using Domain.DataAccess.Repository;
using Domain.DataModel;
using Domain.Dto._Base;
using Domain.Dto.Employee;

namespace DataAccess.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        #region Constructor

        private readonly IBaseRepository<Employee> _repository;

        public EmployeeRepository
        (
            IBaseRepository<Employee> repository
        )
        {
            _repository = repository;
        }

        #endregion

        #region List

        public async Task<PagedResultDto<Employee>> ListAsync(EmployeePagedQueryDto dto)
        {
            var sql = $"SELECT * FROM {Sql.Table<Employee>().ToTableName()} ";
            sql += GetListConditions(dto);
            return await _repository.GetPagedAsync(sql, dto, dto.PageSize, dto.PageIndex, dto.OrderField, null);
        }

        private string GetListConditions(EmployeePagedQueryDto dto)
        {
            var condition = "WHERE 1=1 AND ";
            if (!string.IsNullOrEmpty(dto.Name))
                condition += $"{nameof(Employee.Name)} LIKE '%{dto.Name}%' ";

            return condition;
        }

        #endregion

        #region Create

        public async Task<int> CreateAsync(Employee model)
        {
            var query = QueryGenerator.CreateInsertQuery<Employee>();
            return await _repository.ExecuteAsync(query, model);
        }

        public async Task<int> CreateBulkAsync(IEnumerable<Employee> list)
        {
            foreach (var item in list)
            {
                await CreateAsync(item);
            }

            return 1;
        }

        #endregion

        #region Edit

        public async Task<int> EditAsync(Employee model)
        {
            var query = QueryGenerator.CreateUpdateQuery<Employee>();
            return await _repository.ExecuteAsync(query, model);
        }

        #endregion

        #region Get

        public async Task<Employee> GetAsync(int id)
        {
            var sql = QueryGenerator.CreateSelectByPrimaryKeyQuery<Employee>();
            return await _repository.QueryFirstOrDefaultAsync(sql, new { Id = id });
        }

        #endregion

        #region Delete

        public async Task<int> DeleteAsync(int id)
        {
            var sql = QueryGenerator.CreateDeleteQuery<Employee>();
            return await _repository.ExecuteAsync(sql, new { Id = id });
        }

        #endregion
    }
}
