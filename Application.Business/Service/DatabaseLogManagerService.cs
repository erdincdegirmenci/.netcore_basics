using Application.Domain.DataAccess.DAO.Interface;
using Application.Domain.DataAccess.Model.DatabaseLog;
using AutoMapper;
using DefineXwork.Library.Business;
using DefineXwork.Library.DataAccess;
using DefineXwork.Library.Logging.Database;
using DefineXwork.Library.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Business.Service
{
    public class DatabaseLogManagerService : BaseService, IDatabaseLogManagerService
    {
        // Sadece frameworkten gelen Database Log Manager üzerinden log yazımı işlemi için kullanılır
        private readonly IDatabaseLogManagerDAO _databaseLogManagerDAO;

        public DatabaseLogManagerService(IDatabaseLogManagerDAO databaseLogManagerDAO)
        {
            _databaseLogManagerDAO = databaseLogManagerDAO;
        }
        public void AddToExternalTransaction(IDatabaseManager databaseManager)
        {
            base.AddToTransaction(databaseManager, _databaseLogManagerDAO);
        }

        public void WriteLog(LogSeverityEnum logSeverityEnum, string transactionId, string log, Exception exception, string source, string user)
        {
            DatabaseLogDAOModel databaseLog = new DatabaseLogDAOModel();
            databaseLog.CreateDate = DateTime.Now;
            databaseLog.CreateUser = user;
            databaseLog.Message = string.Format("Message : {0} | Exception : {1} ", log.ToString(), exception != null ? exception.ToString() : "");
            databaseLog.Severity = logSeverityEnum.ToString();
            databaseLog.Source = source;

            _databaseLogManagerDAO.WriteLog(databaseLog);
        }
    }
}
