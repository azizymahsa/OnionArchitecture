using Domain.DataModel;
using Domain.Dto._Base;
using Domain.Dto.Employee;

namespace Domain.DataAccess.Repository
{
    public interface IEmployeeRepository
    {
        Task<PagedResultDto<Employee>> ListAsync(EmployeePagedQueryDto dto);

        Task<int> CreateAsync(Employee model);

        Task<int> CreateBulkAsync(IEnumerable<Employee> list);

        Task<int> EditAsync(Employee model);

        Task<Employee> GetAsync(int id);

        Task<int> DeleteAsync(int id);
    }
}
