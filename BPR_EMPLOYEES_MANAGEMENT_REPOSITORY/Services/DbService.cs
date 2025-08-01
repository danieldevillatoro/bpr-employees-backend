using BPR_EMPLOYEES_MANAGEMENT_CORE.Enumerations;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Interface.Configuration;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Configuration;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Response;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPR_EMPLOYEES_MANAGEMENT_INFRASTRUCTURE.Services
{
    public class DbService : IDbService
    {
        private readonly ResponseBD _responseBD = new();
        private SqlConnection sql = new();
        private SqlCommand cmd = new();
        private readonly ConfigurationMessages _messagesDefault;
        private readonly ILogService _logService;
        private readonly IParseService _parseService;

        public DbService(IOptions<ConfigurationMessages> messagesDefault, ILogService logService, IParseService parseService)
        {
            _messagesDefault = messagesDefault.Value;
            _logService = logService;
            _parseService = parseService;
        }

        private async Task InitConnectionAsync(string cs, int timeout, string sp, IDictionary<string, object> parameters)
        {
            sql = new(cs);
            cmd = new(sp, sql)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = timeout
            };
            foreach (var item in parameters)
            {
                cmd.Parameters.AddWithValue(item.Key, item.Value);
            }
            await sql.OpenAsync();
        }
        private async Task CloseConnectionsAsync()
        {
            await sql.CloseAsync();
            await cmd.DisposeAsync();
            await sql.DisposeAsync();
        }
        public async Task<ResponseBD> ExecSpDataAsync(string cs, int timeout, string sp, IDictionary<string, object> parameters)
        {
            DataTable dt = new();
            try
            {
                _logService.SaveLogApp($"[Service {nameof(ExecSpDataAsync)} - REQUEST DB] :{cs}| Timeout:{timeout}| SP: {sp}| Parameters: {_parseService.Serialize(parameters)}", LogType.Information);
                await InitConnectionAsync(cs, timeout, sp, parameters);
                var reader = await cmd.ExecuteReaderAsync();
                dt.Load(reader);
                await CloseConnectionsAsync();
                await reader.CloseAsync();

                _responseBD.Data = dt;
                _responseBD.ResponseCode = ResponseCode.Success;
                _responseBD.DescriptionResponseCode = _messagesDefault.SuccesMessage;
                _logService.SaveLogApp($"[Service {nameof(ExecSpDataAsync)} - RESPONSE DB] Data :{_responseBD.Data}| Response Code : {_responseBD.ResponseCode}| Description : {_responseBD.DescriptionResponseCode}", LogType.Information);

            }
            catch (SqlException ex)
            {
                _responseBD.ResponseCode = ResponseCode.Error;
                ///_responseBD.DescriptionResponseCode = GetMessageError(ex.Message);
                _responseBD.DescriptionResponseCode = ex.Message;

                _logService.SaveLogApp($"{nameof(ExecSpDataAsync)}{nameof(SqlException)} {ex.Message}", LogType.Information);
            }
            catch (TimeoutException ex)
            {
                _responseBD.ResponseCode = ResponseCode.TimeOut;
                _responseBD.DescriptionResponseCode = _messagesDefault.TimeoutMessage;
                _logService.SaveLogApp($"{nameof(ExecSpDataAsync)}{nameof(TimeoutException)} {ex.Message}", LogType.Information);
            }
            catch (Exception ex)
            {
                _responseBD.ResponseCode = ResponseCode.FatalError;
                _responseBD.DescriptionResponseCode = _messagesDefault.FatalErrorMessage;
                _logService.SaveLogApp($"{nameof(ExecSpDataAsync)}{nameof(Exception)} {ex.Message} | {ex.StackTrace}", LogType.Error);
            }
            finally
            {
                dt.Dispose();
                await CloseConnectionsAsync();
            }
            return _responseBD;
        }
        public async Task<ResponseBD> ExecSpAsync(string cs, int timeout, string sp, IDictionary<string, object> parameters)
        {
            using SqlConnection sql = new(cs);
            try
            {
                _logService.SaveLogApp($"[Service {nameof(ExecSpDataAsync)} - REQUEST DB] :{cs}| Timeout:{timeout}| SP: {sp}| Parameters: {parameters}", LogType.Information);
                await InitConnectionAsync(cs, timeout, sp, parameters);
                await cmd.ExecuteNonQueryAsync();
                await CloseConnectionsAsync();

                _responseBD.ResponseCode = ResponseCode.Success;
                _responseBD.DescriptionResponseCode = _messagesDefault.SuccesMessage;
                _logService.SaveLogApp($"[Service {nameof(ExecSpDataAsync)} - RESPONSE DB] Response Code : {_responseBD.ResponseCode}| Description : {_responseBD.DescriptionResponseCode}", LogType.Information);
            }
            catch (SqlException ex)
            {
                _responseBD.ResponseCode = ResponseCode.Error;
                //_responseBD.DescriptionResponseCode = GetMessageError(ex.Message);
                _responseBD.DescriptionResponseCode = ex.Message;
                _logService.SaveLogApp($"{nameof(ExecSpAsync)}{nameof(SqlException)} {ex.Message}", LogType.Information);
            }
            catch (TimeoutException ex)
            {
                _responseBD.ResponseCode = ResponseCode.TimeOut;
                _responseBD.DescriptionResponseCode = _messagesDefault.TimeoutMessage;
                _logService.SaveLogApp($"{nameof(ExecSpAsync)}{nameof(TimeoutException)} {ex.Message}", LogType.Information);
            }
            catch (Exception ex)
            {
                _responseBD.ResponseCode = ResponseCode.FatalError;
                _responseBD.DescriptionResponseCode = _messagesDefault.FatalErrorMessage;
                _logService.SaveLogApp($"{nameof(ExecSpAsync)}{nameof(Exception)} {ex.Message} | {ex.StackTrace}", LogType.Error);
            }
            finally
            {
                await CloseConnectionsAsync();
            }

            return _responseBD;
        }
        private string GetMessageError(string messageError)
        {
            string[] errors;
            string finalMessage;
            try
            {
                errors = messageError.Split("|");
                if (errors.Length > 1) finalMessage = errors[1];
                else finalMessage = _messagesDefault.FatalErrorMessage;
            }
            catch (Exception)
            {
                finalMessage = _messagesDefault.FatalErrorMessage;
            }
            return finalMessage;
        }
    }
}
