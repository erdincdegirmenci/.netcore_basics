using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.DataAccess.Model.DatabaseLog
{
    public class DatabaseLogDAOModel
    {
        public string Message { get; set; }
        public string TransactionId { get; set; }
        public string Severity { get; set; }
        public string Source { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
    }
}
