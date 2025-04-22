using Application.Business.Model.Response.Parameter;
using DefineXwork.Library.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Business.Service.Interface
{
    public interface IParameterService : IService
    {
        List<GetParameterBusinessResponseModel> GetParameters(int type,string group);
    }
}
