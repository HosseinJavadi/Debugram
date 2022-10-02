using Debugram.Common.AppConfig;
using Debugram.Services.JWTServices;
using Debugram.WebFramework.Middleweres;
using Debugram.WebFramework.ServiceConfiguration;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("SqlServer");
// Add services to the container.


var AppConfigConfiguration = builder.Configuration.GetSection("AppConfig").Get<AppConfig>();

builder.Services.Configure<AppConfig>(builder.Configuration.GetSection("AppConfig"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IJWTService, JWTService>();
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
