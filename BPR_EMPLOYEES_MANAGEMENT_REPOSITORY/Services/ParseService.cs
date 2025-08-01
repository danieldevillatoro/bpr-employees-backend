using BPR_EMPLOYEES_MANAGEMENT_CORE.Interface.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPR_EMPLOYEES_MANAGEMENT_INFRASTRUCTURE.Services
{
    public class ParseService : IParseService
    {
        public T Deserealize<T>(string model) => JsonConvert.DeserializeObject<T>(model);
        public string Serialize(object model) => JsonConvert.SerializeObject(model);
    }
}
