using AutoMapper;
using Application.Business.Model.Response.Authentication;
using Application.Business.Model.Response.Parameter;
using Application.Business.Model.Response.User;
using Application.Business.Model.Response.UserContext;
using Application.Domain.DataAccess.Model.Authentication;
using Application.Domain.DataAccess.Model.Parameter;
using Application.Domain.DataAccess.Model.User;
using Application.Domain.DataAccess.Model.UserContext;
using Application.Domain.DataAccess.Model.UserVerification;
using DefineXwork.Library.Security.Common;
using System.Collections.Generic;
using Application.Business.Model.Cache;
using Application.Business.Model.Request.User;

public class BusinessMapper : Profile
{
    public BusinessMapper()
    {
        CreateMap<UserContextUserDAOModel, UserContextModel>().ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
        CreateMap<UserContextUserDAOModel, UserDetailBusinessResponseModel>();
        CreateMap<CreateUserDAOModel, CreateUserBusinessResponseModel>();
        CreateMap<LoginDAOModel, LoginBusinessResponseModel>();
        CreateMap<LoginDAOModel, LoginByRefreshTokenBusinessResponseModel>();
        CreateMap<GetUserDAOModel, GetActiveUserInfoBusinessResponseModel>();
        CreateMap<CreateUserBusinessRequestModel, CreateUserDAOModel>();
        CreateMap<RegisterUserBusinessRequestModel, CreateUserBusinessRequestModel>();


        #region UserVerification
        CreateMap<CreateUserVerificationBusinessRequestModel, CreateUserVerificationDAOModel>();
        CreateMap<CreateUserVerificationDAOModel, CreateUserVerificationBusinessResponseModel>();
        CreateMap<UpdateUserVerificationBusinessRequestModel, UpdateUserVerificationDAOModel>();
        CreateMap<GetUserVerificationDAOModel, GetUserVerificationBusinessResponseModel>();
        CreateMap<ForgotPasswordVerificationBusinessRequestModel, CreateUserVerificationDAOModel>();
        
        #endregion

        #region Parameter
        CreateMap<ParameterCacheModel, GetParameterBusinessResponseModel>();
        CreateMap<ParameterDAOModel, ParameterCacheModel>();
        #endregion




    }
}