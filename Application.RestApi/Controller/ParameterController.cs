
using System.Collections.Generic;

using AutoMapper;

using Application.Business.Model.Response.Parameter;
using Application.Business.Service.Interface;
using Application.RestApi.Model.Request.Parameter;
using Application.RestApi.Model.Response;
using Application.RestApi.Model.Response.Parameter;
using DefineXwork.Library.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Application.RestApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParameterController : BaseController
    {
        private readonly ILogManager<ParameterController> _logManager;
        private readonly IParameterService _parameterService;
        private readonly IMapper _mapper;

        public ParameterController(ILogManager<ParameterController> logManager, IParameterService parameterService, IMapper mapper)
        {
            _logManager = logManager;
            _parameterService = parameterService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("GetParameters")]
        [Authorize]
        public IActionResult GetParameters([FromBody] ParameterRestReqestModel request)
        {
            RestResponseContainer<List<ParameterRestResponseModel>> response = new RestResponseContainer<List<ParameterRestResponseModel>>();
            List<GetParameterBusinessResponseModel> serviceResponse = _parameterService.GetParameters(request.Type,request.Group);


            response.IsSucceed = true;
            response.Response = _mapper.Map<List<GetParameterBusinessResponseModel>, List<ParameterRestResponseModel>>(serviceResponse);

            return Ok(response);
        }

       
    }
}
