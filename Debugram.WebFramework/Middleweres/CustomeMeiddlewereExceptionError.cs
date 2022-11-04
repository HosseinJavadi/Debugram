using Debugram.Common.CustomeException;
using Debugram.Common.Enums;
using Debugram.WebFramework.Principles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using System.Net;

namespace Debugram.WebFramework.Middleweres
{
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomeMeiddlewereExceptionError>();
        }
    }
    public class CustomeMeiddlewereExceptionError : Exception
    {
        private readonly RequestDelegate _next;
        private readonly IHostingEnvironment _env;
        private readonly ILogger<CustomeMeiddlewereExceptionError> _logger;

        public CustomeMeiddlewereExceptionError(RequestDelegate next,
            IHostingEnvironment env,
            ILogger<CustomeMeiddlewereExceptionError> logger)
        {
            _next = next;
            _env = env;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            string message = null;
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            ResultApiStatusCode apiStatusCode = ResultApiStatusCode.ServerError;

            try
            {
                await _next(context);
            }
            catch (AppException exception)
            {
                apiStatusCode = exception.ResultApiStatusCode;
                httpStatusCode = exception.HttpStatusCode;
                    message = exception.Message;
                await WriteToResponseAsync();
            }
            catch (SecurityTokenExpiredException exception)
            {

                _logger.LogError(exception, exception.Message);
                SetUnAuthorizeResponse(exception);
                await WriteToResponseAsync();
            }
            catch (UnauthorizedAccessException exception)
            {

                _logger.LogError(exception, exception.Message);
                SetUnAuthorizeResponse(exception);
                await WriteToResponseAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);

                await WriteToResponseAsync();
            }

            async Task WriteToResponseAsync()
            {
                if (context.Response.HasStarted)
                    throw new InvalidOperationException("The response has already started, the http status code middleware will not be executed.");

                var result = new ApiResult(false, apiStatusCode, message);
                var setting = new JsonSerializerSettings() {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                var json = JsonConvert.SerializeObject(result,setting);

                context.Response.StatusCode = (int)httpStatusCode;
                context.Response.ContentType = "application/json;charset=utf-8";
                await context.Response.WriteAsync(json);
            }

            void SetUnAuthorizeResponse(Exception exception)
            {
                httpStatusCode = HttpStatusCode.Unauthorized;
                apiStatusCode = ResultApiStatusCode.UnAuthorized;

                if (_env.IsDevelopment())
                {
                    var dic = new Dictionary<string, string>
                    {
                        ["Exception"] = exception.Message,
                        ["StackTrace"] = exception.StackTrace
                    };
                    if (exception is SecurityTokenExpiredException tokenException)
                        dic.Add("Expires", tokenException.Expires.ToString());

                    message = JsonConvert.SerializeObject(dic);
                }
            }

        }
    }
}
