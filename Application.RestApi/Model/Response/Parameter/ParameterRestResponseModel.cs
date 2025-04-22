using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.RestApi.Model.Response.Parameter
{
    public class ParameterRestResponseModel
    {
        public int Type { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public string Group { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
    }
}
