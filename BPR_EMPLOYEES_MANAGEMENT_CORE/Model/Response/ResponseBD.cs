using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Response
{
    public class ResponseBD : BaseResponse
    {
        public DataTable Data { get; set; }
    }
}
