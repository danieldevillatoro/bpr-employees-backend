using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Request
{
    public class HttpServiceRequest
    {
        public Dictionary<string, string> Headers = new Dictionary<string, string>();
        public Uri Url { get; set; }
        public string Body { get; set; } = string.Empty;
        public double TimeOut { get; set; }
        public string Method { get; set; } = string.Empty;

    }
}
