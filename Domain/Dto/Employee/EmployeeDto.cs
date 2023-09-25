using Domain.Enumeration;

namespace Domain.DataModel
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Payroll { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? FormDate { get; set; }
        public DateTime? OnboardDate { get; set; }
        public DateTime? LeaveDate { get; set; }
        public string Workplace { get; set; }
        public EmployeeStatus Status { get; set; }
        public string Team { get; set; }
        public string DomainSquad { get; set; }
        public string GovernanceHandler { get; set; }
        public string VFBizFormNo { get; set; }
        public OTPStatus OTPStatus { get; set; }
        public string AccessForms { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string GSMNo { get; set; }
        public int? SAPSicilNo { get; set; }
        public string LaptopVdiForm { get; set; }
        public DateTime? LaptopVdiFormDate { get; set; }
        public string LaptopVdiId { get; set; }
        public string LaptopModel { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Notes { get; set; }
        public string AmdocsSAPCode { get; set; }
    }
}
