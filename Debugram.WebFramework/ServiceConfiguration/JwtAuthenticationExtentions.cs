using Debugram.Common.AppConfig;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;


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
                    TokenDecryptionKey = new SymmetricSecurityKey(encryptKey)
                };

                options.SaveToken = jwtSetting.SaveToken;
                options.TokenValidationParameters = tokenValidationParameters;
                options.RequireHttpsMetadata = jwtSetting.RequireHttpsMetadata;
             
            });
        }
    }
}
