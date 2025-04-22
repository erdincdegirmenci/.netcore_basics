using System;
using System.Text;
using Application.Business.Service;
using Application.Business.Service.Interface;
using Application.Common.Extension;
using Application.Common.Helper;
using Application.Common.Models;
using Application.Domain.DataAccess.DAO;
using Application.Domain.DataAccess.DAO.Interface;
using Application.Domain.DataAccess.Query;
using Application.RestApi.Common.Extension;
using Application.RestApi.Common.Helper;
using Application.RestApi.Common.Mapper;
using Application.RestApi.Filter;
using DefineXwork.Library.Api.Swagger;
using DefineXwork.Library.Caching;
using DefineXwork.Library.Caching.MemoryCache;
using DefineXwork.Library.Configuration;
using DefineXwork.Library.DataAccess.Helper;
using DefineXwork.Library.DataAccess.Manager;
using DefineXwork.Library.Integration;
using DefineXwork.Library.Logging;
using DefineXwork.Library.Logging.Database;
using DefineXwork.Library.Security;
using DefineXwork.Library.Security.Common;
using DefineXwork.Library.Security.Jwt;
using DefineXwork.Library.Transaction;
using DefineXwork.Library.Utility.Swagger.Attribute;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Application.RestApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        private readonly JwtOptions _jwtOptions;
        private readonly AppSettings _appSettings;
       // private readonly ConsulClientConfig _consulClientConfig;

        private string[] _corsOrigins = { };
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _jwtOptions = Configuration.GetSection("JwtOptions").Get<JwtOptions>();
            _appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();
            //_consulClientConfig = Configuration.GetSection("Consul").Get<ConsulClientConfig>();

            string corsOriginValue = Configuration.GetValue<string>("AllowedOrigin");

            if (!string.IsNullOrEmpty(corsOriginValue))
            {
                _corsOrigins = corsOriginValue.Split(',', StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                //Consul ile kullanıldığında açılacak
                //services.AddConsulConfig(_consulClientConfig);

                //GRPC
                //services.AddGrpc(options =>
                //{
                //    {
                //        // CustomGrpcExceptionFilter ==>> DefineX.Labs.LegalTech.ClientApi.Grpc.Filter namespece i altındaki CustomGrpcExceptionFilter.cs
                //        options.Interceptors.Add<CustomGrpcExceptionFilter>();
                //        options.EnableDetailedErrors = true;
                //    }
                //});

                //KafkaConsumer ==> DefineX.Labs.LegalTech.ClientApi.Kafka.Service namespace i altındaki KafkaConsumer.cs class
                //Kafka kullanıldığında açılacak
                //services.AddHostedService<KafkaConsumer>(); 
                //services.AddTransient<IConsumerManager, ConsumerManager>();
                //services.AddTransient<IProducerManager, ProducerManager>();

                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            builder.WithOrigins(_corsOrigins).AllowAnyHeader().WithMethods("GET", "PUT", "POST", "DELETE", "UPDATE", "OPTIONS");
                        });
                });

                services.AddControllers(
                    opt =>
                    {
                        opt.Filters.Add(new ProducesAttribute("application/json"));
                    }
                    ).AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _jwtOptions.Issuer,
                        ValidAudience = _jwtOptions.Audience,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey)),
                    };
                    options.EventsType = typeof(CustomJwtEventHandler);
                });

                services.AddApiVersioning(
                    o =>
                    {
                        //o.Conventions.Controller<UserController>().HasApiVersion(1, 0);
                        o.ReportApiVersions = true;
                        o.AssumeDefaultVersionWhenUnspecified = true;
                        o.DefaultApiVersion = new ApiVersion(1, 0);
                        o.ApiVersionReader = new UrlSegmentApiVersionReader();
                    }
                    );

                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });

                //SWAGGER
                if (_appSettings.Swagger.Enabled)
                {
                    services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

                    services.AddSwaggerGen(options =>
                    {
                        options.OperationFilter<SwaggerDefaultValues>();
                        //options.IncludeXmlComments(XmlCommentsFilePath);
                    });
                }
                //  logger.LogDebug("Startup::ConfigureServices::ApiVersioning, Swagger and DI settings");

                //Email Helper
                services.Configure<MailSettings>(Configuration.GetSection("EmailConfiguration"));
                services.AddTransient<EmailHelper>();

                services.AddHttpContextAccessor();
                services.AddDistributedMemoryCache();

                services.AddSession();

                services.AddMemoryCache();

                services.AddAutoMapper(typeof(RestMapper),typeof(BusinessMapper));
                services.AddLogging();

                // Dapper Property Mapper
                TypeMapper.Initialize("Business.Domain.DataAccess.Model");

                //ELK loglama icin
                //services.AddScoped(typeof(ILogManager<>), typeof(ElkLogManager<>));

                services.AddScoped(typeof(ILogManager<>),typeof(DatabaseLogManager<>));

                services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
                services.AddSingleton<IConfigManager, ConfigHelper>();


                services.AddTransient<ITransactionContextManager, TransactionContextManager>();
                services.AddScoped<ICacheManager, MemoryCacheManager>();
                services.AddTransient<IHttpContextHelper, HttpContextHelper>();

                services.AddTransient<IUserContextModel, UserContextModel>();

                services.AddTransient(typeof(IUserContextManager<IUserContextModel>), typeof(UserContextManager));
                services.AddTransient<IJwtTokenHandler, JwtTokenHandler>();
                services.AddTransient<JwtHelper>();
                services.AddScoped<JwtOptions>();
                services.AddTransient<IUserContextService, UserContextService>();
                services.AddTransient<IAuthenticationService, AuthenticationService>();
                services.AddTransient<IDatabaseLogManagerService, DatabaseLogManagerService>();
                services.AddTransient<IUserService, UserService>();
                services.AddTransient<IParameterService, ParameterService>();


                services.AddTransient<IDatabaseLogManagerDAO, DatabaseLogManagerDAO>(x => new DatabaseLogManagerDAO(new MysqlDatabaseManager("DBConnection", x.GetService<IConfigManager>()), new DatabaseLogManagerQueryTemplate()));
                services.AddTransient<IUserContextDAO, UserContextDAO>(x => new UserContextDAO(new MysqlDatabaseManager("DBConnection", x.GetService<IConfigManager>()), new UserContextQueryTemplate()));
                services.AddTransient<IAuthenticationDAO, AuthenticationDAO>(x => new AuthenticationDAO(new MysqlDatabaseManager("DBConnection", x.GetService<IConfigManager>()), new AuthenticationQueryTemplate()));

                services.AddTransient<IUserDAO, UserDAO>(x => new UserDAO(new MysqlDatabaseManager("DBConnection", x.GetService<IConfigManager>()), new UserQueryTemplate()));
                services.AddTransient<IUserVerificationDAO, UserVerificationDAO>(x => new UserVerificationDAO(new MysqlDatabaseManager("DBConnection", x.GetService<IConfigManager>()), new UserVerificationQueryTemplate()));
                services.AddTransient<IParameterDAO, ParameterDAO>(x => new ParameterDAO(new MysqlDatabaseManager("DBConnection", x.GetService<IConfigManager>()), new ParameterQueryTemplate()));


                //services.AddTransient<IRestIntegration, RestIntegration>();
                //services.AddTransient<ISoapIntegration, SoapIntegration>();
                services.AddTransient<CustomJwtEventHandler>();
                services.AddScoped<CustomExceptionFilter>();

                ConfigHelper.LoadConfig(Configuration);
                MemoryCacheManager.LocalCacheKey = Configuration.GetValue<string>("AppSettings:LocalCacheKey");

            }
            catch (Exception ex)
            {
                //Debug da hatayı alabilmek icin
                throw ex;
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger, IApiVersionDescriptionProvider provider)
        {
            //Consul ile kullanıldığında açılacak
            //app.UseConsul();

            logger.LogTrace("Startup::Configure");
            logger.LogDebug($"Startup::Configure::Environment:{env.EnvironmentName}");
            try
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    // app.UseMiddleware(typeof(ErrorHandlingMiddleware));
                    app.UseHsts();
                }

                app.UseCors();

                app.UseSession();


                //app.UseHttpsRedirection();
                app.UseRouting();
                app.UseAuthentication();
                app.UseAuthorization();



                app.UseMiddleware(typeof(SecurityMiddleware));

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
                app.UseRequestLocalization();

             

                //SWAGGER
                if (_appSettings.Swagger.Enabled)
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(options =>
                    {
                        foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
                        {
                            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                            //options.RoutePrefix = string.Empty;
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw ex;
            }
        }
    }
}
