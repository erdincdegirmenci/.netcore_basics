using Application.Domain.DataAccess.DAO.Interface;
using Application.Domain.DataAccess.Model.DatabaseLog;
using DefineXwork.Library.DataAccess;


namespace Application.Domain.DataAccess.DAO
{
    public class DatabaseLogManagerDAO : BaseDAO<IDatabaseManager>, IDatabaseLogManagerDAO
    {

        // Sadece frameworkten gelen Database Log Manager üzerinden log yazımı işlemi için kullanılır

        public DatabaseLogManagerDAO(IDatabaseManager databaseManager) : base(databaseManager)
        {

        }
        public DatabaseLogManagerDAO(IDatabaseManager databaseManager, IQueryTemplate queryTemplate) : base(databaseManager, queryTemplate)
        {

        }

        public void WriteLog(DatabaseLogDAOModel databaseLog)
        {
            base.InsertWithTemplate("DatabaseLogDAO.AddLog", databaseLog);
        }
    }
}
