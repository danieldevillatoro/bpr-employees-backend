using BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Request;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Response;

namespace BPR_EMPLOYEES_MANAGEMENT_CORE.Interface.Configuration
{
    public interface IHttpService
    {
        Task<HttpServiceResponse<T>> GET<T>(HttpServiceRequest request);
        Task<HttpServiceResponse<T>> POST<T>(HttpServiceRequest request);
    }
}
