using Application.Domain.DataAccess.Model.DatabaseLog;
using DefineXwork.Library.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.DataAccess.DAO.Interface
{
    public interface IDatabaseLogManagerDAO : IDAO
    {
        void WriteLog(DatabaseLogDAOModel databaseLog);
    }
}
