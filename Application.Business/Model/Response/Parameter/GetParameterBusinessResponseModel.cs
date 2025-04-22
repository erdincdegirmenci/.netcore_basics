using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Business.Model.Response.Parameter
{
    public class GetParameterBusinessResponseModel
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public string Group { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
    }
}
