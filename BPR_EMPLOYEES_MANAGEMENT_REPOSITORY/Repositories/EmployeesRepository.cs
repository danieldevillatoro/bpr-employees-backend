using BPR_EMPLOYEES_MANAGEMENT_CORE.Enumerations;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Interface.Configuration;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Interface.ConnectionDB;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Interface.Models;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Configuration;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Request;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Response;
using Microsoft.Extensions.Options;
using System.Data;

namespace BPR_EMPLOYEES_MANAGEMENT_INFRASTRUCTURE.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly Dictionary<string, dynamic> _bdParameters;
        private ResponseBD _responseBD;
        private readonly IEmployeesCallSPRepository _npsRepository;
        private string _sp_Employees = "_sp_Employees";
        private string _sp_GetHierarchy = "_sp_GetHierarchy";
        private readonly ILogService _logService;
        private readonly ConfigurationMessages _messagesDefault;

        public EmployeesRepository(ILogService logService, IConfigurationService configurationService, IOptions<ConfigurationMessages> messagesDefault, IEmployeesCallSPRepository npsRepository)
        {
            _bdParameters = new Dictionary<string, dynamic>();
            _messagesDefault = messagesDefault.Value;
            _logService = logService;
            _npsRepository = npsRepository;
        }

        public async Task<ResponseAPI<List<EmployeeResponse>>> GetEmployees()
        {
            _bdParameters.Clear();
            ResponseAPI<List<EmployeeResponse>> responseAPI = new();
            List<EmployeeResponse> paramss = new();

            try
            {
                _bdParameters.Add("@i_operation", "GET_ALL_EMPLOYEES");
                _responseBD = await _npsRepository.CallSPData(_sp_Employees, _bdParameters);
                responseAPI.ResponseCode = _responseBD.ResponseCode;

                if (_responseBD.ResponseCode != ResponseCode.Success)
                {
                    _logService.SaveLogApp($"[{nameof(GetEmployees)} - Exception] {_responseBD.ResponseCode} | {_responseBD.DescriptionResponseCode}",
                        LogType.Information);
                    responseAPI.ResponseCode = _responseBD.ResponseCode;
                    responseAPI.DescriptionResponseCode = _responseBD.DescriptionResponseCode;
                    return responseAPI;
                }

                foreach (DataRow dr in _responseBD.Data.Rows)
                {
                    paramss.Add(new EmployeeResponse()
                    {

                        id = dr["ID"] != DBNull.Value ? Convert.ToString(dr["ID"]) : string.Empty,
                        name = dr["NAME"] != DBNull.Value ? Convert.ToString(dr["NAME"]) : string.Empty,
                        charge = dr["CHARGE"] != DBNull.Value ? Convert.ToString(dr["CHARGE"]) : string.Empty,
                        idBoss = dr["ID_BOSS"] != DBNull.Value ? Convert.ToString(dr["ID_BOSS"]) : string.Empty,
                        status = dr["STATUS"] != DBNull.Value ? Convert.ToString(dr["STATUS"]) : string.Empty
                    });
                }

                responseAPI.Data = paramss;
                responseAPI.ResponseCode = ResponseCode.Success;
                responseAPI.DescriptionResponseCode = _messagesDefault.SuccesMessage;

            }
            catch (Exception ex)
            {
                responseAPI.ResponseCode = ResponseCode.FatalError;
                responseAPI.DescriptionResponseCode = _messagesDefault.FatalErrorMessage;
                _logService.SaveLogApp($"[{nameof(GetEmployees)} - Exception] {ex.Message} | {ex.StackTrace}", LogType.Error);
            }
            finally
            {
                _bdParameters.Clear();
            }
            return responseAPI;
        }
        
        
        public async Task<ResponseAPI<List<EmployeeHierarchy>>> GetHierarchy()
        {
            _bdParameters.Clear();
            ResponseAPI<List<EmployeeHierarchy>> responseAPI = new();
            List<EmployeeHierarchy> paramss = new();

            try
            {
                _responseBD = await _npsRepository.CallSPData(_sp_GetHierarchy, _bdParameters);
                responseAPI.ResponseCode = _responseBD.ResponseCode;

                if (_responseBD.ResponseCode != ResponseCode.Success)
                {
                    _logService.SaveLogApp($"[{nameof(GetEmployees)} - Exception] {_responseBD.ResponseCode} | {_responseBD.DescriptionResponseCode}",
                        LogType.Information);
                    responseAPI.ResponseCode = _responseBD.ResponseCode;
                    responseAPI.DescriptionResponseCode = _responseBD.DescriptionResponseCode;
                    return responseAPI;
                }

                foreach (DataRow dr in _responseBD.Data.Rows)
                {
                    paramss.Add(new EmployeeHierarchy()
                    {

                        Hierarchy = dr["Hierarchy"] != DBNull.Value ? Convert.ToString(dr["Hierarchy"]) : string.Empty,
                        Level = dr["LEVEL"] != DBNull.Value ? Convert.ToString(dr["LEVEL"]) : string.Empty
                    });
                }

                responseAPI.Data = paramss;
                responseAPI.ResponseCode = ResponseCode.Success;
                responseAPI.DescriptionResponseCode = _messagesDefault.SuccesMessage;

            }
            catch (Exception ex)
            {
                responseAPI.ResponseCode = ResponseCode.FatalError;
                responseAPI.DescriptionResponseCode = _messagesDefault.FatalErrorMessage;
                _logService.SaveLogApp($"[{nameof(GetEmployees)} - Exception] {ex.Message} | {ex.StackTrace}", LogType.Error);
            }
            finally
            {
                _bdParameters.Clear();
            }
            return responseAPI;
        }
        
        public async Task<ResponseAPI<List<EmployeeResponse>>> GetEmployee(EmployeeRequest request)
        {
            _bdParameters.Clear();
            ResponseAPI<List<EmployeeResponse>> responseAPI = new();
            List<EmployeeResponse> paramss = new();

            try
            {
                _bdParameters.Add("@i_operation", "GET_SPECIFIC_EMPLOYEE");
                _bdParameters.Add("@i_id", request.id);
                _responseBD = await _npsRepository.CallSPData(_sp_Employees, _bdParameters);
                responseAPI.ResponseCode = _responseBD.ResponseCode;

                if (_responseBD.ResponseCode != ResponseCode.Success)
                {
                    _logService.SaveLogApp($"[{nameof(GetEmployees)} - Exception] {_responseBD.ResponseCode} | {_responseBD.DescriptionResponseCode}",
                        LogType.Information);
                    responseAPI.ResponseCode = _responseBD.ResponseCode;
                    responseAPI.DescriptionResponseCode = _responseBD.DescriptionResponseCode;
                    return responseAPI;
                }

                foreach (DataRow dr in _responseBD.Data.Rows)
                {
                    paramss.Add(new EmployeeResponse()
                    {

                        id = dr["ID"] != DBNull.Value ? Convert.ToString(dr["ID"]) : string.Empty,
                        name = dr["NAME"] != DBNull.Value ? Convert.ToString(dr["NAME"]) : string.Empty,
                        charge = dr["CHARGE"] != DBNull.Value ? Convert.ToString(dr["CHARGE"]) : string.Empty,
                        idBoss = dr["ID_BOSS"] != DBNull.Value ? Convert.ToString(dr["ID_BOSS"]) : string.Empty
                    });
                }

                responseAPI.Data = paramss;
                responseAPI.ResponseCode = ResponseCode.Success;
                responseAPI.DescriptionResponseCode = _messagesDefault.SuccesMessage;

            }
            catch (Exception ex)
            {
                responseAPI.ResponseCode = ResponseCode.FatalError;
                responseAPI.DescriptionResponseCode = _messagesDefault.FatalErrorMessage;
                _logService.SaveLogApp($"[{nameof(GetEmployees)} - Exception] {ex.Message} | {ex.StackTrace}", LogType.Error);
            }
            finally
            {
                _bdParameters.Clear();
            }
            return responseAPI;
        }
        
        
        
        
        public async Task<ResponseAPI<EmployeeResponse>> CreateEmployee(EmployeeRequest request)
        {
            _bdParameters.Clear();
            ResponseAPI<EmployeeResponse> responseAPI = new();
            try
            {
                _bdParameters.Add("@i_operation", "INSERT_EMPLOYEES");
                _bdParameters.Add("@i_name", request.name);
                _bdParameters.Add("@i_charge", request.charge);
                _bdParameters.Add("@i_id_boss", request.idBoss);
                _responseBD = await _npsRepository.CallSP(_sp_Employees, _bdParameters);
                responseAPI.ResponseCode = _responseBD.ResponseCode;

                if (_responseBD.ResponseCode != ResponseCode.Success)
                {
                    _logService.SaveLogApp($"[{nameof(CreateEmployee)} - Exception] {_responseBD.ResponseCode} | {_responseBD.DescriptionResponseCode}",
                        LogType.Information);
                    responseAPI.ResponseCode = _responseBD.ResponseCode;
                    responseAPI.DescriptionResponseCode = _responseBD.DescriptionResponseCode;
                    return responseAPI;
                }
                responseAPI.ResponseCode = ResponseCode.Success;
                responseAPI.DescriptionResponseCode = _messagesDefault.SuccesMessage;

            }
            catch (Exception ex)
            {
                responseAPI.ResponseCode = ResponseCode.FatalError;
                responseAPI.DescriptionResponseCode = _messagesDefault.FatalErrorMessage;
                _logService.SaveLogApp($"[{nameof(CreateEmployee)} - Exception] {ex.Message} | {ex.StackTrace}", LogType.Error);
            }
            finally
            {
                _bdParameters.Clear();
            }
            return responseAPI;
        }
        
        
        public async Task<ResponseAPI<EmployeeResponse>>  UpdateEmployee(EmployeeRequest request)
        {
            _bdParameters.Clear();
            ResponseAPI<EmployeeResponse> responseAPI = new();
            try
            {
                _bdParameters.Add("@i_operation", "UPDATE_EMPLOYEES");
                _bdParameters.Add("@i_id", request.id);
                _bdParameters.Add("@i_name", request.name);
                _bdParameters.Add("@i_charge", request.charge);
                _bdParameters.Add("@i_id_boss", request.idBoss);
                _responseBD = await _npsRepository.CallSP(_sp_Employees, _bdParameters);
                responseAPI.ResponseCode = _responseBD.ResponseCode;

                if (_responseBD.ResponseCode != ResponseCode.Success)
                {
                    _logService.SaveLogApp($"[{nameof(UpdateEmployee)} - Exception] {_responseBD.ResponseCode} | {_responseBD.DescriptionResponseCode}",
                        LogType.Information);
                    responseAPI.ResponseCode = _responseBD.ResponseCode;
                    responseAPI.DescriptionResponseCode = _responseBD.DescriptionResponseCode;
                    return responseAPI;
                }
                responseAPI.ResponseCode = ResponseCode.Success;
                responseAPI.DescriptionResponseCode = _messagesDefault.SuccesMessage;

            }
            catch (Exception ex)
            {
                responseAPI.ResponseCode = ResponseCode.FatalError;
                responseAPI.DescriptionResponseCode = _messagesDefault.FatalErrorMessage;
                _logService.SaveLogApp($"[{nameof(UpdateEmployee)} - Exception] {ex.Message} | {ex.StackTrace}", LogType.Error);
            }
            finally
            {
                _bdParameters.Clear();
            }
            return responseAPI;
        }
        
        
        public async Task<ResponseAPI<EmployeeResponse>>  DeleteEmployee(EmployeeRequest request)
        {
            _bdParameters.Clear();
            ResponseAPI<EmployeeResponse> responseAPI = new();
            try
            {
                _bdParameters.Add("@i_operation", "DELETE_EMPLOYEE");
                _bdParameters.Add("@i_id", request.id);
                _responseBD = await _npsRepository.CallSP(_sp_Employees, _bdParameters);
                responseAPI.ResponseCode = _responseBD.ResponseCode;

                if (_responseBD.ResponseCode != ResponseCode.Success)
                {
                    _logService.SaveLogApp($"[{nameof(DeleteEmployee)} - Exception] {_responseBD.ResponseCode} | {_responseBD.DescriptionResponseCode}",
                        LogType.Information);
                    responseAPI.ResponseCode = _responseBD.ResponseCode;
                    responseAPI.DescriptionResponseCode = _responseBD.DescriptionResponseCode;
                    return responseAPI;
                }
                responseAPI.ResponseCode = ResponseCode.Success;
                responseAPI.DescriptionResponseCode = _messagesDefault.SuccesMessage;

            }
            catch (Exception ex)
            {
                responseAPI.ResponseCode = ResponseCode.FatalError;
                responseAPI.DescriptionResponseCode = _messagesDefault.FatalErrorMessage;
                _logService.SaveLogApp($"[{nameof(DeleteEmployee)} - Exception] {ex.Message} | {ex.StackTrace}", LogType.Error);
            }
            finally
            {
                _bdParameters.Clear();
            }
            return responseAPI;
        }
    }
}
