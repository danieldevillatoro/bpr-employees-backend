using BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Request;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPR_EMPLOYEES_MANAGEMENT_CORE.Interface.Models
{
    public interface IEmployeesRepository
    {
        Task<ResponseAPI<List<EmployeeResponse>>> GetEmployees();
        Task<ResponseAPI<List<EmployeeResponse>>> GetEmployee(EmployeeRequest request);
        Task<ResponseAPI<EmployeeResponse>> CreateEmployee(EmployeeRequest request);
        Task<ResponseAPI<EmployeeResponse>> UpdateEmployee(EmployeeRequest request);
        Task<ResponseAPI<EmployeeResponse>> DeleteEmployee(EmployeeRequest request);
        Task<ResponseAPI<List<EmployeeHierarchy>>> GetHierarchy();
    }
}
