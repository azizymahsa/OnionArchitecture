using Domain.DataModel;
using Domain.Dto._Base;
using Domain.Dto.Employee;
using Shared.Model.Employee;

namespace Shared.Mapping
{
    public static class EmployeeMapping
    {
        #region EmployeeViewModel => EmployeeDto

        public static EmployeeDto ToDto(this EmployeeViewModel model)
        {
            return new EmployeeDto
            {
                AccessForms = model.AccessForms,
                BirthDate = model.BirthDate,
                Country = model.Country,
                DomainSquad = model.DomainSquad,
                Email = model.Email,
                FormDate = model.FormDate,
                GovernanceHandler = model.GovernanceHandler,
                GSMNo = model.GSMNo,
                Id = model.Id,
                LaptopModel = model.LaptopModel,
                LaptopVdiForm = model.LaptopVdiForm,
                LaptopVdiFormDate = model.LaptopVdiFormDate,
                LaptopVdiId = model.LaptopVdiId,
                LeaveDate = model.LeaveDate,
                Name = model.Name,
                Notes = model.Notes,
                OnboardDate = model.OnboardDate,
                OTPStatus = model.OTPStatus,
                Payroll = model.Payroll,
                RequestDate = model.RequestDate,
                SAPSicilNo = model.SAPSicilNo,
                Status = model.Status,
                Team = model.Team,
                Username = model.Username,
                VFBizFormNo = model.VFBizFormNo,
                Workplace = model.Workplace,
                AmdocsSAPCode = model.AmdocsSAPCode
            };
        }

        #endregion

        #region EmployeeDto => EmployeeViewModel

        public static EmployeeViewModel ToViewModel(this EmployeeDto model)
        {
            return new EmployeeViewModel
            {
                AccessForms = model.AccessForms,
                BirthDate = model.BirthDate,
                Country = model.Country,
                DomainSquad = model.DomainSquad,
                Email = model.Email,
                FormDate = model.FormDate,
                GovernanceHandler = model.GovernanceHandler,
                GSMNo = model.GSMNo,
                Id = model.Id,
                LaptopModel = model.LaptopModel,
                LaptopVdiForm = model.LaptopVdiForm,
                LaptopVdiFormDate = model.LaptopVdiFormDate,
                LaptopVdiId = model.LaptopVdiId,
                LeaveDate = model.LeaveDate,
                Name = model.Name,
                Notes = model.Notes,
                OnboardDate = model.OnboardDate,
                OTPStatus = model.OTPStatus,
                Payroll = model.Payroll,
                RequestDate = model.RequestDate,
                SAPSicilNo = model.SAPSicilNo,
                Status = model.Status,
                Team = model.Team,
                Username = model.Username,
                VFBizFormNo = model.VFBizFormNo,
                Workplace = model.Workplace,
                AmdocsSAPCode = model.AmdocsSAPCode
            };
        }

        #endregion

        #region IEnumerable<EmployeeDto> => IEnumerable<EmployeeViewModel>

        public static IEnumerable<EmployeeViewModel> ToViewModelList(this IEnumerable<EmployeeDto> model) => model.Select(ToViewModel);

        #endregion

        #region IEnumerable<EmployeeViewModel> => IEnumerable<EmployeeDto>

        public static IEnumerable<EmployeeDto> ToDtoList(this IEnumerable<EmployeeViewModel> model) => model.Select(ToDto);

        #endregion

        #region PagedResultDto<EmployeeDto> => PagedResultDto<EmployeeViewModel>

        public static PagedResultDto<EmployeeViewModel> ToPagedResultViewModel(this PagedResultDto<EmployeeDto> model)
        {
            return new PagedResultDto<EmployeeViewModel>
            {
                List = model.List.ToViewModelList(),
                Total = model.Total
            };
        }

        #endregion

        #region EmployeePagedQueryViewModel => EmployeePagedQueryDto

        public static EmployeePagedQueryDto ToDto(this EmployeePagedQueryViewModel model)
        {
            return new EmployeePagedQueryDto
            {
                Name = model?.Name?.Trim(),
                OrderField = model?.OrderField?.Trim(),
                PageIndex = model.PageIndex,
                PageSize = model.PageSize
            };
        }

        #endregion
    }
}
