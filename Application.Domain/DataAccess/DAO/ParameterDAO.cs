using Application.Domain.DataAccess.DAO.Interface;
using Application.Domain.DataAccess.Model.Parameter;
using DefineXwork.Library.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Domain.DataAccess.DAO
{
    public class ParameterDAO : BaseDAO<IDatabaseManager>, IParameterDAO
    {
        public ParameterDAO(IDatabaseManager databaseManager) : base(databaseManager)
        {

        }
        public ParameterDAO(IDatabaseManager databaseManager, IQueryTemplate queryTemplate) : base(databaseManager, queryTemplate)
        {

        }
        public List<ParameterDAOModel> GetParameters()
        {
            return base.SelectWithTemplate<ParameterDAOModel>("ParameterDAO.GetAllParameters").ToList();

        }
    }
}
