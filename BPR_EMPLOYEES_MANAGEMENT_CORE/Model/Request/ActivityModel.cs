namespace BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Request
{
    public class ActivityModel
    {
        public string ActivityId { get; set; } = string.Empty;
        public string OperationId { get; set; } = string.Empty;
        public string JsonRequest { get; set; } = string.Empty;
        public string JsonResponse { get; set; } = string.Empty;
        public string CreateDatetime { get; set; } = string.Empty;
        public string UpdateDatetime { get; set; } = string.Empty;
    }
}
