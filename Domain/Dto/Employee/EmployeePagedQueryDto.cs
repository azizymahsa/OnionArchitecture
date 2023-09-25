using Domain.Dto._Base;

namespace Domain.Dto.Employee
{
    public class EmployeePagedQueryDto : PagedQueryDto
    {
        public string Name { get; set; }
    }
}
