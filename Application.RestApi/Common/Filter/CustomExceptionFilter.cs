
using DefineXwork.Library.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using System.ServiceModel.Security;
using Application.RestApi.Model.Response;
using DefineXwork.Library.Security;

namespace Application.RestApi.Filter
{
    public class CustomExceptionFilter : ExceptionFilterAttribute, IExceptionFilter
    {
        private readonly ILogManager<CustomExceptionFilter> _logManager;
        private readonly ITransactionContextManager _transactionContextManager;
        public CustomExceptionFilter(ILogManager<CustomExceptionFilter> logManager, ITransactionContextManager transactionContextManager)
        {
            _logManager = logManager;
            _transactionContextManager = transactionContextManager;

        }
        public override void OnException(ExceptionContext context)
        {

            ControllerActionDescriptor controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            int statusCode;

            /*if (context.Exception is HttpResponseException)
            {
                // Internal Server  = 500, Unauthorized = 401
                statusCode = context.HttpContext.Response.StatusCode;
            }*/

            if (context.Exception is ArgumentNullException) statusCode = (int)HttpStatusCode.BadRequest;
            else if (context.Exception is ArgumentException) statusCode = (int)HttpStatusCode.BadRequest;
            else if (context.Exception is UnauthorizedAccessException) statusCode = (int)HttpStatusCode.Unauthorized;
            else if (context.Exception is SecurityAccessDeniedException) statusCode = (int)HttpStatusCode.Forbidden;
            else // özel hatalarda
            {
                statusCode = (int)HttpStatusCode.InternalServerError;
            }

            string methodDescriptor = string.Format("{0}.{1}.{2}", controllerActionDescriptor.MethodInfo.ReflectedType.Namespace,
                controllerActionDescriptor.MethodInfo.ReflectedType.Name,
                controllerActionDescriptor.MethodInfo.Name);

            RestResponseContainer<object> responseModel = new RestResponseContainer<object>
            {
                Response = null,
                IsSucceed = false,
                ErrorCode = Convert.ToInt32(statusCode).ToString(),
                ErrorMessage = context.Exception.Message.ToString(),
                TransactionCode = _transactionContextManager.GetTransaction()
            };

            ObjectResult result = new ObjectResult(responseModel)
            {
                StatusCode = statusCode
            };

            context.Result = result;

            _logManager.LogError($"Exception Message : {context.Exception.Message} || methodDescriptor : {methodDescriptor} {context.Exception.StackTrace}");

        }
    }
}
