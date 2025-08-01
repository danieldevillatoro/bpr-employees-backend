using BPR_EMPLOYEES_MANAGEMENT_CORE.Enumerations;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Interface.Configuration;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Interface.Models;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Request;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BPR_EMPLOYEES_MANAGEMENT.Controllers
{
    [ApiController]
    [Route("api/v1/employees/")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesService _service;
        private readonly ILogger<EmployeesController> _logger;
        private readonly ILogService _logService;
        public EmployeesController(ILogger<EmployeesController> logger, IEmployeesService service, ILogService logService)
        {
            _logger = logger;
            _service = service;
            _logService = logService;
        }


        [Route("GetEmployees"), HttpPost]
        [HttpPost]
        public async Task<ActionResult<ResponseAPI<List<EmployeeResponse>>>> GetEmployees()
        {
            ResponseAPI<List<EmployeeResponse>> response = new ResponseAPI<List<EmployeeResponse>>();
            try
            {
                response = await _service.GetEmployees();
            }
            catch (Exception ex)
            {
                _logService.SaveLogApp($"[{nameof(GetEmployees)} - {nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);
            }
            return response;
        }

        [Route("GetEmployee"), HttpPost]
        [HttpPost]
        public async Task<ActionResult<ResponseAPI<List<EmployeeResponse>>>> GetEmployee([FromBody] EmployeeRequest request)
        {
            ResponseAPI<List<EmployeeResponse>> response = new ResponseAPI<List<EmployeeResponse>>();
            try
            {
                response = await _service.GetEmployee(request);
            }
            catch (Exception ex)
            {
                _logService.SaveLogApp($"[{nameof(GetEmployees)} - {nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);
            }
            return response;
        }


        [Route("CreateEmployee"), HttpPost]
        [HttpPost]
        public async Task<ActionResult<ResponseAPI<EmployeeResponse>>> CreateEmployee([FromBody] EmployeeRequest request)
        {
            ResponseAPI<EmployeeResponse> response = new ResponseAPI<EmployeeResponse>();
            try
            {
                response = await _service.CreateEmployee(request);
            }
            catch (Exception ex)
            {
                _logService.SaveLogApp($"[{nameof(GetEmployees)} - {nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);
            }
            return response;
        }


        [Route("UpdateEmployee"), HttpPost]
        [HttpPost]
        public async Task<ActionResult<ResponseAPI<EmployeeResponse>>> UpdateEmployee([FromBody] EmployeeRequest request)
        {
            ResponseAPI<EmployeeResponse> response = new ResponseAPI<EmployeeResponse>();
            try
            {
                response = await _service.UpdateEmployee(request);
            }
            catch (Exception ex)
            {
                _logService.SaveLogApp($"[{nameof(UpdateEmployee)} - {nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);
            }
            return response;
        }

        [Route("DeleteEmployee"), HttpPost]
        [HttpPost]
        public async Task<ActionResult<ResponseAPI<EmployeeResponse>>> DeleteEmployee([FromBody] EmployeeRequest request)
        {
            ResponseAPI<EmployeeResponse> response = new ResponseAPI<EmployeeResponse>();
            try
            {
                response = await _service.DeleteEmployee(request);
            }
            catch (Exception ex)
            {
                _logService.SaveLogApp($"[{nameof(UpdateEmployee)} - {nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);
            }
            return response;
        }


        [Route("GetHierarchy"), HttpPost]
        [HttpPost]
        public async Task<ActionResult<ResponseAPI<List<EmployeeHierarchy>>>> GetHierarchy()
        {
            ResponseAPI<List<EmployeeHierarchy>> response = new ResponseAPI<List<EmployeeHierarchy>>();
            try
            {
                response = await _service.GetHierarchy();
            }
            catch (Exception ex)
            {
                _logService.SaveLogApp($"[{nameof(UpdateEmployee)} - {nameof(Exception)}] {ex.Message}|{ex.StackTrace}", LogType.Error);
            }
            return response;
        }
    }
}
