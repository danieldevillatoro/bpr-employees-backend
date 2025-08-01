using BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPR_EMPLOYEES_MANAGEMENT_CORE.Interface.Configuration
{
    public interface IDbService
    {
        Task<ResponseBD> ExecSpAsync(string cs, int timeout, string sp, IDictionary<string, object> parameters);
        Task<ResponseBD> ExecSpDataAsync(string cs, int timeout, string sp, IDictionary<string, object> parameters);
    }
}
