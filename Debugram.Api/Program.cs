using Debugram.Common.AppConfig;
using Debugram.CommonModel.AutoMapper;
using Debugram.Data.Context;
using Debugram.Data.Contracts;
using Debugram.Data.Repositories;
using Debugram.Data.Service.IService.Account;
using Debugram.Data.Service.Service.Account;
using Debugram.Service.ModelValidation;
using Debugram.Services.JWTServices;
using Debugram.WebFramework.Authorize;
using Debugram.WebFramework.Middleweres;
using Debugram.WebFramework.ServiceConfiguration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
try
{
    logger.Debug("Init Main");
    var builder = WebApplication.CreateBuilder(args);

    string connectionString = builder.Configuration.GetConnectionString("Debugram");
    // Add services to the container.


    var AppConfigConfiguration = builder.Configuration.GetSection("AppConfig").Get<AppConfig>();

    builder.Host.UseNLog();
    //builder.Host.ConfigureLogging(n => n.ClearProviders());

    builder.Services.Configure<AppConfig>(builder.Configuration.GetSection("AppConfig"));
    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

    builder.Services.AddDbContext<ApplicationDbContext>(n => n.UseSqlServer(connectionString));
    builder.Services.AddScoped<IJWTService, JWTService>();
    builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
    builder.Services.AddSingleton<IAutoMapperConfiguration, AutoMapperConfiguration>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IAccountService, AccountService>();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddJwtAuthentication(AppConfigConfiguration.JwtSetting);
    builder.Services.AddCors();
    builder.Services.AddControllers(
       options =>
       {
           options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
           options.Filters.Add(typeof(ApiModelValidation));

           var policy = new AuthorizationPolicyBuilder()
                 .RequireAuthenticatedUser()
                 .Build();
           options.Filters.Add(new ApiAuthorize(AppConfigConfiguration,policy));
       });

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseMiddleware<CustomeMeiddlewereExceptionError>();
    app.UseCors(option =>
    {
        option.AllowAnyOrigin();
        option.AllowAnyMethod();
        option.AllowAnyHeader();
    });
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseRouting();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex,"Error StartUp");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}

