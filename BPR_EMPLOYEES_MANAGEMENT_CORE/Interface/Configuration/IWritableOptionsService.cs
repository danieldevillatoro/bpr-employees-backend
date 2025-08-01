using Microsoft.Extensions.Options;

namespace BPR_EMPLOYEES_MANAGEMENT_CORE.Interface.Configuration
{
    public interface IWritableOptionsService<out T> : IOptions<T> where T : class, new()
    {
        void Update(Action<T> applyChanges);
    }
}
