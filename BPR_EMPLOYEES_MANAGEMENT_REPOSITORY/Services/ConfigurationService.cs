using BPR_EMPLOYEES_MANAGEMENT_CORE.Interface.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPR_EMPLOYEES_MANAGEMENT_INFRASTRUCTURE.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;
        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public T Get<T>(string section) => _configuration.GetSection(section).Get<T>();
    }
}
