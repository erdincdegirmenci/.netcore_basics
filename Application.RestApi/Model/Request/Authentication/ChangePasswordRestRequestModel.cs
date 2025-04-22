using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.RestApi.Model.Request.Authentication
{
    public class ChangePasswordRestRequestModel
    {
        public string Password { get; set; }
        public string VerificationCode { get; set; }
    }
}
