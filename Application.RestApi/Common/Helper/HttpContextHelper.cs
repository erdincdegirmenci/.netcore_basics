using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;

namespace Application.RestApi.Common.Helper
{
    public class HttpContextHelper : IHttpContextHelper
    {
        private readonly IActionContextAccessor _accesor;
        public HttpContextHelper(IActionContextAccessor accessor)
        {
            _accesor = accessor;
        }
        public HttpContextModel GetHttpContext()
        {
            HttpContextModel httpContextModel = new HttpContextModel
            {
                ClientIp = _accesor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString(),
                ServerIp = _accesor.ActionContext.HttpContext.Connection.LocalIpAddress.ToString(),
                ServerName = Environment.MachineName,
                Url = _accesor.ActionContext.HttpContext.Request.GetEncodedUrl()
            };

            return httpContextModel;
        }
    }
}
