﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Response
{
    public class EmployeeResponse
    {
        public string id { get; set; }
        public string name { get; set; }
        public string charge { get; set; }
        public string idBoss { get; set; }
        public string status { get; set; }
    }
}
