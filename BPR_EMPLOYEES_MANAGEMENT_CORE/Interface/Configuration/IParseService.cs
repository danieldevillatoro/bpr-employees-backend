namespace BPR_EMPLOYEES_MANAGEMENT_CORE.Interface.Configuration
{
    public interface IParseService
    {
        T Deserealize<T>(string model);
        string Serialize(object model);
    }
}
