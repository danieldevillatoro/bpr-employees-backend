namespace BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Configuration
{
    public class ConfigurationDB
    {
        public DBModel Company { get; set; }
    }

    public class DBModel
    {
        public string Server { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public int TimeOut { get; set; }
    }
}
