using Application.Domain.DataAccess.Model.Parameter;
using DefineXwork.Library.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.DataAccess.DAO.Interface
{
    public interface IParameterDAO : IDAO
    {
        List<ParameterDAOModel> GetParameters();
    }
}
