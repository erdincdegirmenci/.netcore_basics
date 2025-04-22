
using AutoMapper;
using DefineXwork.Library.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.RestApi.Model.Response;
using Application.Common.Const;
using Application.RestApi.Model.Logger;

namespace Application.RestApi.Controller
{
    [Route("api/[controller]")]
    public class LoggerController : BaseController
    {
        private readonly ILogManager<LoggerController> _logManager;
        private readonly IMapper _mapper;

        public LoggerController(ILogManager<LoggerController> logManager, IMapper mapper)
        {
            _logManager = logManager;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("WriteLog")]
        [Authorize]
        public IActionResult WriteLog([FromBody] LogRestRequestModel request)
        {
            RestResponseContainer<EmptyRestResponseModel> response = new RestResponseContainer<EmptyRestResponseModel>();

            if (request.Type == LogTypes.ERROR)
            {
                _logManager.LogError(request.TransactionId, request.Log, request.User);
            }
            else if (request.Type == LogTypes.WARNING)
            {
                _logManager.LogWarning(request.TransactionId, request.Log, request.User);
            }
            else
            {
                _logManager.LogInfo(request.TransactionId, request.Log, request.User);
            }
            response.IsSucceed = true;
            return Ok(response);
        }

    }
}
