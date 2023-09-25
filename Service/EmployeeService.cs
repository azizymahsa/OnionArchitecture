using Domain.DataAccess.Repository;
using Domain.DataModel;
using Domain.Dto._Base;
using Domain.Dto.Employee;
using Domain.Mapping;
using Domain.Service;
using OfficeOpenXml;

namespace Service
{
    public class EmployeeService : IEmployeeService
    {
        #region Constructor

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEPPlusService _epPlusService;

        public EmployeeService
        (
            IEmployeeRepository employeeRepository,
            IEPPlusService epPlusService
        )
        {
            _employeeRepository = employeeRepository;
            _epPlusService = epPlusService;
        }

        #endregion

        #region List

        public async Task<PagedResultDto<EmployeeDto>> ListAsync(EmployeePagedQueryDto dto)
        {
            return (await _employeeRepository.ListAsync(dto)).ToPagedResultDto();
        }

        #endregion

        #region Create

        public async Task<int> CreateAsync(EmployeeDto dto)
        {
            return await _employeeRepository.CreateAsync(dto.ToDataModel());
        }

        #endregion

        #region Edit

        public async Task<int> EditAsync(EmployeeDto dto)
        {
            if (await GetAsync(dto.Id) != null)
                return await _employeeRepository.EditAsync(dto.ToDataModel());

            return 0;
        }

        #endregion

        #region Get

        public async Task<EmployeeDto> GetAsync(int id)
        {
            return (await _employeeRepository.GetAsync(id)).ToDto();
        }

        #endregion

        #region Delete

        public async Task<int> DeleteAsync(int id)
        {
            if (await GetAsync(id) != null)
                return await _employeeRepository.DeleteAsync(id);

            return 0;
        }

        #endregion

        #region Excel

        public async Task<bool> ImportExcel(UploadFileDto dto)
        {
            var list = await ExcelFıleToEmployeeList(dto);

            foreach (var item in list)
                await CreateAsync(item);

            return true;
        }

        public Task<byte[]> ExportExcel()
        {
            throw new NotImplementedException();
        }

        private async Task<IEnumerable<EmployeeDto>> ExcelFıleToEmployeeList(UploadFileDto dto)
        {
            await using MemoryStream memStream = new(dto.FileContent);
            ExcelPackage package = new(memStream);
            var workSheet = package.Workbook.Worksheets[0];

            var employeeExcelDtoList = _epPlusService.ConvertSheetToObjects<EmployeeExcelDto>(workSheet);
            var dtoList = employeeExcelDtoList.ToEmployeeDtoList();

            return dtoList;
        }

        #endregion
    }
}
                