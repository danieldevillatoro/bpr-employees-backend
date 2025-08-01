using BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Response;

namespace BPR_EMPLOYEES_MANAGEMENT_CORE.Interface.ConnectionDB
{
    public interface IEmployeesCallSPRepository
    {
        Task<ResponseBD> CallSP(string sp, Dictionary<string, dynamic> parameters);
        Task<ResponseBD> CallSPData(string sp, Dictionary<string, dynamic> parameters);
    }
}
