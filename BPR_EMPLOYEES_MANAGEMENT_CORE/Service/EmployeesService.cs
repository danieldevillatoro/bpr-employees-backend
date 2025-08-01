using BPR_EMPLOYEES_MANAGEMENT_CORE.Enumerations;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Interface.Configuration;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Interface.Models;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Request;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Response;

namespace BPR_EMPLOYEES_MANAGEMENT_CORE.Service
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _repository;


        private readonly ILogService _logService;

        public EmployeesService(ILogService logService, IEmployeesRepository repository)
        {
            _logService = logService;
            _repository = repository;
        }


        public async Task<ResponseAPI<List<EmployeeResponse>>> GetEmployees()
        {
            ResponseAPI<List<EmployeeResponse>> responseAPI = new ResponseAPI<List<EmployeeResponse>>();
            try
            {
                responseAPI = await _repository.GetEmployees();
            }
            catch (Exception ex)
            {
                _logService.SaveLogApp($"[{nameof(GetEmployees)} - {nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);
            }
            return responseAPI;

        }

        public async Task<ResponseAPI<List<EmployeeResponse>>> GetEmployee(EmployeeRequest request)
        {
            ResponseAPI<List<EmployeeResponse>> responseAPI = new ResponseAPI<List<EmployeeResponse>>();
            try
            {
                responseAPI = await _repository.GetEmployee(request);
            }
            catch (Exception ex)
            {
                _logService.SaveLogApp($"[{nameof(GetEmployee)} - {nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);
            }
            return responseAPI;

        }

        public async Task<ResponseAPI<EmployeeResponse>> CreateEmployee(EmployeeRequest request)
        {
            ResponseAPI<EmployeeResponse> responseAPI = new ResponseAPI<EmployeeResponse>();
            try
            {
                responseAPI = await _repository.CreateEmployee(request);
            }
            catch (Exception ex)
            {
                _logService.SaveLogApp($"[{nameof(CreateEmployee)} - {nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);
            }
            return responseAPI;

        }

        public async Task<ResponseAPI<EmployeeResponse>> UpdateEmployee(EmployeeRequest request)
        {
            ResponseAPI<EmployeeResponse> responseAPI = new ResponseAPI<EmployeeResponse>();
            try
            {
                responseAPI = await _repository.UpdateEmployee(request);
            }
            catch (Exception ex)
            {
                _logService.SaveLogApp($"[{nameof(UpdateEmployee)} - {nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);
            }
            return responseAPI;

        }
        
        
        public async Task<ResponseAPI<EmployeeResponse>> DeleteEmployee(EmployeeRequest request)
        {
            ResponseAPI<EmployeeResponse> responseAPI = new ResponseAPI<EmployeeResponse>();
            try
            {
                responseAPI = await _repository.DeleteEmployee(request);
            }
            catch (Exception ex)
            {
                _logService.SaveLogApp($"[{nameof(UpdateEmployee)} - {nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);
            }
            return responseAPI;

        }

        public async Task<ResponseAPI<List<EmployeeHierarchy>>> GetHierarchy()
        {
            ResponseAPI<List<EmployeeHierarchy>> responseAPI = new ResponseAPI<List<EmployeeHierarchy>>();
            try
            {
                responseAPI = await _repository.GetHierarchy();
            }
            catch (Exception ex)
            {
                _logService.SaveLogApp($"[{nameof(UpdateEmployee)} - {nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);
            }
            return responseAPI;
        }
    }
}
