using Application.RestApi.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Application.RestApi.Controller
{

    [ApiController]
    [ServiceFilter(typeof(CustomExceptionFilter))]
    public abstract class BaseController : ControllerBase
    {
    }
}
