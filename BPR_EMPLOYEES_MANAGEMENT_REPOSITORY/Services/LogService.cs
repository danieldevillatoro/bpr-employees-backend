using BPR_EMPLOYEES_MANAGEMENT_CORE.Enumerations;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Interface.Configuration;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPR_EMPLOYEES_MANAGEMENT_INFRASTRUCTURE.Services
{
    public class LogService : ILogService
    {
        static readonly object _locker = new object();
        private readonly ConfigurationLog _configurationLog;

        public LogService(IConfigurationService configurationService)
        {
            _configurationLog = configurationService.Get<ConfigurationLog>(BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Configuration.ConfigurationSection.LogService);
        }
        private static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public void SaveLogApp(string message, LogType logType)
        {
            string type;
            if (logType == LogType.Service)
                type = "Service";
            else if (logType == LogType.Error)
                type = "Error";
            else
                type = "Information";

            string path = $"{_configurationLog.Path}{ConfigurationLog.Date}\\{type}\\";
            if (Environment.OSVersion.Platform == PlatformID.Unix)
                path = path.Replace("\\", "/");

            try
            {

                CreateDirectory(path);
                string line = $"[{DateTime.Now:dd/MM/yyyy} {DateTime.Now:HH:mm:ss.fff}]";
                line = $"{line}: {message}{Environment.NewLine}";
                lock (_locker)
                {
                    File.AppendAllText($"{path}{_configurationLog.NameFile}", line);
                }

            }
            catch (Exception)
            {

            }
        }
    }
}
