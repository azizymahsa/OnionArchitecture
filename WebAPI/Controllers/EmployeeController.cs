using Domain.Service;
using Microsoft.AspNetCore.Mvc;
using Shared.Mapping;
using Shared.Model._Base;
using Shared.Model.Employee;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    public class EmployeeController : ApiControllerBase
    {
        #region Constractor

        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        #endregion

        #region List

        [HttpGet("List")]
        public async Task<IActionResult> List([FromQuery] EmployeePagedQueryViewModel viewModel)
        {
            return Ok(await _employeeService.ListAsync(viewModel.ToDto()));
        }

        #endregion

        #region Create

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] EmployeeViewModel viewModel)
        {
            var result = await _employeeService.CreateAsync(viewModel.ToDto());
            return result > 0 ? Ok(true) : BadRequest(false);
        }

        #endregion

        #region Edit

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit([FromBody] EmployeeViewModel viewModel)
        {
            var result = await _employeeService.EditAsync(viewModel.ToDto());
            return result > 0 ? Ok(true) : BadRequest(false);
        }

        #endregion

        #region Detail

        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            var result = await _employeeService.GetAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        #endregion

        #region Delete

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var result = await _employeeService.DeleteAsync(id);
            return result > 0 ? Ok(true) : NotFound();
        }

        #endregion

        #region Excel

        [HttpPost("ImportExcel")]
        public async Task<IActionResult> ImportExcel(UploadFileViewModel viewModel)
        {
            var result = await _employeeService.ImportExcel(viewModel.ToDto());
            return result ? Ok(true) : NotFound();
        }

        [HttpGet("ExportExcel")]
        public async Task<IActionResult> ExportExcel()
        {
            var result = await _employeeService.ExportExcel();
            return result != null ? Ok(true) : NotFound();
        }

        #endregion
    }
}
