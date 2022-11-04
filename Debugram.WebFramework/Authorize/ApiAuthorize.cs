using Debugram.Common.AppConfig;
using Debugram.Common.CustomeException;
using Debugram.Common.Enums;
using Debugram.Common.Utilities;
using Debugram.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace Debugram.WebFramework.Authorize
{
    public class ApiAuthorize : AuthorizeFilter
    {
        private readonly AppConfig appSetting;


        public ApiAuthorize(AppConfig appSetting, AuthorizationPolicy policy) :
            base(policy)
        {
            this.appSetting = appSetting;
        }
        public override  Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var attributeDatas = descriptor.MethodInfo.GetCustomAttributesData();
            if(attributeDatas.Any(n=>n.AttributeType.Name == "AllowAnonymousAttribute"))
                return base.OnAuthorizationAsync(context);



            var token = context.HttpContext.Request.Headers["token"].ToString();

            dynamic _userRepository = context.HttpContext.RequestServices.GetService(typeof(IUserRepository))!;

            if (string.IsNullOrEmpty(token))
                context.Result = new ForbidResult();



            var secretKey = Encoding.UTF8.GetBytes(appSetting.JwtSetting.SecretKey);
            var encryptKey = Encoding.UTF8.GetBytes(appSetting.JwtSetting.EncryptKey);

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ClockSkew = TimeSpan.Zero,
                RequireSignedTokens = appSetting.JwtSetting.RequireSignedTokens,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                ValidateIssuerSigningKey = appSetting.JwtSetting.ValidateIssuerSigningKey,

                RequireExpirationTime = appSetting.JwtSetting.RequireExpirationTime,
                ValidateLifetime = appSetting.JwtSetting.ValidateLifetime,

                ValidateAudience = appSetting.JwtSetting.ValidateAudience,
                ValidAudience = appSetting.JwtSetting.ValidAudience,
                ValidIssuer = appSetting.JwtSetting.ValidIssuer,
                ValidateIssuer = appSetting.JwtSetting.ValidateIssuer,
                TokenDecryptionKey = new SymmetricSecurityKey(encryptKey),
            };
            try
            {
                var responseObj = new
                {
                    IsSuccess = false,
                    Message = ResultApiStatusCode.InValidParameters.ToDisplay(),
                    Errors = new List<string>(),
                    Status = ResultApiStatusCode.InValidParameters
                };


                SecurityToken validatedToken;
                var handler = new JwtSecurityTokenHandler();
                IPrincipal principal = handler.ValidateToken(token, tokenValidationParameters, out validatedToken);

                var userId = SecurityHelper.Decrypt(principal.Identity!.GetUserId(), appSetting.PrivateKey);
                
                var securitystamptype = new ClaimsIdentityOptions().SecurityStampClaimType;

                var stamp = principal.Identity?.FindFirstValue(securitystamptype);
                var user = _userRepository!.GetById(long.Parse(userId!));

                if (user == null)
                {
                    responseObj.Errors.Add("کاربری با این مشخصات وجود ندارد ");
                    context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(responseObj)
                    {
                        StatusCode = 401
                    };
                    return Task.FromResult<object>(responseObj);
                }

                if (user!.SecurityStamp! != Guid.Parse(stamp))
                {
                    responseObj.Errors.Add("اطلاعات شما بروز شده لطفا اقدام به احراز هویت کنید");
                    context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(responseObj)
                    {
                        StatusCode = 401
                    };
                    return Task.FromResult<object>(responseObj);
                }

                var identity = principal.Identity as ClaimsIdentity;
                var existingClaim = identity!.FindFirst(ClaimTypes.NameIdentifier);
                identity.RemoveClaim(existingClaim);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
                context.HttpContext.User = new System.Security.Claims.ClaimsPrincipal(identity);
            }
            catch (Exception ex)
            {

                throw new AppException(ResultApiStatusCode.UnAuthorized, ResultApiStatusCode.UnAuthorized.ToDisplay(), System.Net.HttpStatusCode.Unauthorized);
            }
            return base.OnAuthorizationAsync(context);
        }
    }
}
