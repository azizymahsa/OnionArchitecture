using Domain.DataModel;
using Domain.Dto._Base;
using Domain.Dto.Employee;

namespace Domain.Service
{
    public interface IEmployeeService
    {
        Task<PagedResultDto<EmployeeDto>> ListAsync(EmployeePagedQueryDto dto);
        Task<int> CreateAsync(EmployeeDto dto);
        Task<int> EditAsync(EmployeeDto dto);
        Task<EmployeeDto> GetAsync(int id);
        Task<int> DeleteAsync(int id);
        Task<bool> ImportExcel(UploadFileDto dto);
        Task<byte[]> ExportExcel();
    }
}
