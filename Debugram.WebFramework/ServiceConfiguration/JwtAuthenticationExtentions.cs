using Debugram.Common.AppConfig;
using Debugram.Data.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Debugram.Common.Utilities;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;

namespace Debugram.WebFramework.ServiceConfiguration
{
    public static class JwtAuthenticationExtentions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, JwtSetting jwtSetting)
        {
            services.AddAuthentication(option =>
            {
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var secretKey = Encoding.UTF8.GetBytes(jwtSetting.SecretKey);
                var encryptKey = Encoding.UTF8.GetBytes(jwtSetting.EncryptKey);
                var tokenValidationParameters = new TokenValidationParameters()
                {
                    ClockSkew = TimeSpan.Zero,
                    RequireSignedTokens = jwtSetting.RequireSignedTokens,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuerSigningKey = jwtSetting.ValidateIssuerSigningKey,

                    RequireExpirationTime = jwtSetting.RequireExpirationTime,
                    ValidateLifetime = jwtSetting.ValidateLifetime,

                    ValidateAudience = jwtSetting.ValidateAudience,
                    ValidAudience = jwtSetting.ValidAudience,
                    ValidIssuer = jwtSetting.ValidIssuer,
                    ValidateIssuer = jwtSetting.ValidateIssuer,
                    TokenDecryptionKey = new SymmetricSecurityKey(encryptKey),
                };

                options.SaveToken = jwtSetting.SaveToken;
                options.TokenValidationParameters = tokenValidationParameters;
                options.RequireHttpsMetadata = jwtSetting.RequireHttpsMetadata;
                //options.Events = new JwtBearerEvents()
                //{
                //    OnAuthenticationFailed = async context =>
                //    {
                //        string token =  context.HttpContext.Request.Cookies["token"]!;
                //        if (string.IsNullOrEmpty(token))
                //            context.Fail("احراز هویت کنید");


                //        var tokenHandler = new JwtSecurityTokenHandler();

                //        SecurityToken validatedToken;
                //        IPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
                //        context.HttpContext.Items.Add("claims", principal);

                //        var UserDb = context.HttpContext.RequestServices.GetService<IUserRepository>();
                //        var userId = context.HttpContext.User.Identity?.GetUserId();
                //        var securitystamptype = new ClaimsIdentityOptions().SecurityStampClaimType;

                //        var stamp = context.HttpContext.User.Identity!.FindFirstValue(securitystamptype);
                //        var user = await UserDb!.GetByIdAsync(context.HttpContext.RequestAborted, userId!);

                //        if (user == null)
                //            context.Fail("کاربری با این شناسه وجود ندارد");

                //        if (user.SecurityStamp!.Value.ToString() != stamp)
                //            context.Fail("اطلاعات بروز شده مجددا اهراز هویت شوید");

                //        context.Success();
                //    },
                //    OnTokenValidated = async (context) =>
                //    {
                //        var UserDb = context.HttpContext.RequestServices.GetService<IUserRepository>();
                //        var userId = context.HttpContext.User.Identity?.GetUserId();
                //        var securitystamptype = new ClaimsIdentityOptions().SecurityStampClaimType;

                //        var stamp = context.HttpContext.User.Identity!.FindFirstValue(securitystamptype);
                //        var user = await UserDb!.GetByIdAsync(context.HttpContext.RequestAborted,userId!);

                //        if (user == null)
                //            context.Fail("کاربری با این شناسه وجود ندارد");

                //        if (user.SecurityStamp!.Value.ToString() != stamp)
                //            context.Fail("اطلاعات بروز شده مجددا اهراز هویت شوید");

                //        context.Success();
                        
                //    }
                //};
            });
        }
    }
}
