using System;

namespace Application.Domain.DataAccess.Model
{
    public class BaseDAOModel
    {
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string LastUpdateUser { get; set; }
    }
}
