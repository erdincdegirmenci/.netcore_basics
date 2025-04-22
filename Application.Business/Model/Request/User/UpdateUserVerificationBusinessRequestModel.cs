using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Business.Model.Request.User
{
    public class UpdateUserVerificationBusinessRequestModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime? VerificationDate { get; set; }
        public bool IsVerificate { get; set; }
    }
}
