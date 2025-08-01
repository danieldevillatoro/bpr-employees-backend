namespace BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Response
{
    public class HttpServiceResponse<T> : BaseResponse
    {
        public T Data { get; set; }
    }
}
