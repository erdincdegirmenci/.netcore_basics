using AutoMapper;
using Application.Business.Model.Request.Authentication;
using Application.Business.Model.Response.Authentication;
using Application.RestApi.Model.Request.Authentication;
using Application.RestApi.Model.Request.Parameter;
using Application.RestApi.Model.Response;
using Application.RestApi.Model.Response.Parameter;
using Application.Business.Model.Request.User;
using Application.Business.Model.Response.Parameter;

namespace Application.RestApi.Common.Mapper
{
    public class RestMapper : Profile
    {
        public RestMapper()
        {


            CreateMap<RegisterRestRequestModel, RegisterUserBusinessRequestModel>();
            CreateMap<LoginBusinessResponseModel, LoginRestResponseModel>();
            CreateMap<LoginByRefreshTokenBusinessResponseModel, LoginRestResponseModel>();
            CreateMap<GetParameterBusinessResponseModel, ParameterRestResponseModel>();

        }
    }
}
