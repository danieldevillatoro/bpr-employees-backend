namespace BPR_EMPLOYEES_MANAGEMENT_CORE.Interface.Configuration
{
    public interface IConfigurationService
    {
        T Get<T>(string section);
    }
}
