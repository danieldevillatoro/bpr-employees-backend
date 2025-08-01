using BPR_EMPLOYEES_MANAGEMENT_CORE.Enumerations;

namespace BPR_EMPLOYEES_MANAGEMENT_CORE.Interface.Configuration
{
    public interface ILogService
    {
        void SaveLogApp(string message, LogType logType);
    }
}
