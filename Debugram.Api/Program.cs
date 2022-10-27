using Debugram.Common.AppConfig;
using Debugram.Common.AutoMapper;
using Debugram.Data.Context;
using Debugram.Data.Contracts;
using Debugram.Data.Repositories;
using Debugram.Data.Service.IService.Account;
using Debugram.Data.Service.Service;
using Debugram.Services.JWTServices;
using Debugram.WebFramework.Middleweres;
using Debugram.WebFramework.ServiceConfiguration;
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

    builder.Services.AddDbContext<ApplicationDbContext>(n => n.UseSqlServer(connectionString));
    builder.Services.AddScoped<IJWTService, JWTService>();
    builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
    builder.Services.AddScoped<IAutoMapperConfiguration, AutoMapperConfiguration>();
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

