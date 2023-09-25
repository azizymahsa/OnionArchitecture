namespace Domain.Dto._Base
{
    public class ApplicationSettingsDto
    {
        public ConnectionString ConnectionStrings { get; set; }
        public string JiraToken { get; set; }
        public string AuthenticationServer { get; set; }
    }

    public class ConnectionString
    {
        public string MySql { get; set; }
        public string Vtas { get; set; }

    }
}
