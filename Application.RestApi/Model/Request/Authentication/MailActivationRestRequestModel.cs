using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.RestApi.Model.Request.Authentication
{
    public class MailActivationRestRequestModel
    {
        public string Mode { get; set; }
        public string VerificationCode { get; set; }
    }
}
