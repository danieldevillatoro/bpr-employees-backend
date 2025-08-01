namespace BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Configuration
{
    public class ConfigurationLog
    {
        public string NameFile { get; set; } = "log.txt";
        public string Path { get; set; } = "Log";
        public static string Date => DateTime.Now.ToString("dd-MM-yyyy");
    }
}
