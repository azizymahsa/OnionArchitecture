using Domain.Attributes;

namespace Domain.Dto.Employee
{
    public class EmployeeExcelDto
    {
        [ExcelColumn(1)]
        public string Name { get; set; }

        [ExcelColumn(2)]
        public string Country { get; set; }

        [ExcelColumn(3)]
        public string Payroll { get; set; }

        [ExcelColumn(4)]
        public string RequestDate { get; set; }

        [ExcelColumn(5)]
        public string FormDate { get; set; }

        [ExcelColumn(6)]
        public string OnboardDate { get; set; }

        [ExcelColumn(7)]
        public string LeaveDate { get; set; }

        [ExcelColumn(8)]
        public string Workplace { get; set; }

        [ExcelColumn(9)]
        public string Status { get; set; }

        [ExcelColumn(10)]
        public string Team { get; set; }

        [ExcelColumn(11)]
        public string DomainSquad { get; set; }

        [ExcelColumn(12)]
        public string GovernanceHandler { get; set; }

        [ExcelColumn(13)]
        public string VFBizFormNo { get; set; }

        [ExcelColumn(14)]
        public string OTPStatus { get; set; }

        [ExcelColumn(15)]
        public string AccessForms { get; set; }

        [ExcelColumn(16)]
        public string Username { get; set; }

        [ExcelColumn(17)]
        public string Email { get; set; }

        [ExcelColumn(18)]
        public string GSMNo { get; set; }

        [ExcelColumn(19)]
        public string SAPSicilNo { get; set; }

        [ExcelColumn(20)]
        public string LaptopVdiForm { get; set; }

        [ExcelColumn(21)]
        public string LaptopVdiFormDate { get; set; }

        [ExcelColumn(22)]
        public string LaptopVdiId { get; set; }

        [ExcelColumn(23)]
        public string LaptopModel { get; set; }

        [ExcelColumn(24)]
        public string BirthDate { get; set; }

        [ExcelColumn(25)]
        public string AmdocsSAPCode { get; set; }

        [ExcelColumn(26)]
        public string Notes { get; set; }
    }
}
