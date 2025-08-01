using BPR_EMPLOYEES_MANAGEMENT_CORE.Interface.Configuration;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Interface.ConnectionDB;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Configuration;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Response;
using Microsoft.Extensions.Options;

namespace BPR_EMPLOYEES_MANAGEMENT_INFRASTRUCTURE.Repositories
{
    public class EmployeesCallSPRepository : IEmployeesCallSPRepository
    {
        private readonly ResponseBD _responseBD;
        private DBModel _notificationDb;
        private readonly IDbService _dbService;
        private readonly string _connectionString;
        private readonly IConfigurationService _configurationService;
        private readonly ILogService _logService;
        private readonly IWritableOptionsService<ConfigurationDB> _writableConfigurationBD;
        private readonly ConfigurationMessages _messagesDefault;


        public EmployeesCallSPRepository(
           ILogService logService,
           IConfigurationService configurationService,
           IWritableOptionsService<ConfigurationDB> configurationBD,
           IOptions<ConfigurationMessages> messagesDefault,
           IDbService dbService
       )
        {
            _logService = logService;
            _configurationService = configurationService;
            _writableConfigurationBD = configurationBD;
            _messagesDefault = messagesDefault.Value;
            _dbService = dbService;


            InitConfiguration();
            _connectionString = $"Server={_notificationDb.Server};Database={_notificationDb.Name};" +
                               $"User Id={_notificationDb.User};Password={_notificationDb.Password}; TrustServerCertificate=True";

            _responseBD = new ResponseBD();
        }

        private void InitConfiguration()
        {
            _notificationDb = _configurationService.Get<ConfigurationDB>(ConfigurationSection.ConnectionStrings).Company;
        }
        public async Task<ResponseBD> CallSP(string sp, Dictionary<string, object> parameters) => await _dbService.ExecSpAsync(_connectionString, _notificationDb.TimeOut, sp, parameters);
        public async Task<ResponseBD> CallSPData(string sp, Dictionary<string, object> parameters) => await _dbService.ExecSpDataAsync(_connectionString, _notificationDb.TimeOut, sp, parameters);

    }
}
