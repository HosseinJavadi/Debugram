using Debugram.Common.AppConfig;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Debugram.Services.JWTServices
{
    public class JWTService : IJWTService
    {
        private readonly AppConfig appConfig;


        public JWTService(IOptionsSnapshot<AppConfig> appConfig)
        {
            this.appConfig = appConfig.Value;
        }
        public async Task<string> Generate(dynamic user) //(dynamic) this will should change
        {
            var secretKey = Encoding.UTF8.GetBytes(appConfig.JwtSetting.SecretKey);
            var signingCredential = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);
            var encryptCredentials = new EncryptingCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var claims = await _getClaims(user);
            var descriptor = new SecurityTokenDescriptor()
            {
                Issuer = appConfig.JwtSetting.ValidIssuer,
                Audience = appConfig.JwtSetting.ValidAudience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(appConfig.JwtSetting.NotBefore),
                Expires = DateTime.Now.AddMinutes(appConfig.JwtSetting.Expires),
                SigningCredentials = signingCredential,
                Subject = new ClaimsIdentity(claims),
                EncryptingCredentials = encryptCredentials,
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);

            var jwt = tokenHandler.WriteToken(securityToken);
            return jwt;
        }


        private async Task<IEnumerable<Claim>> _getClaims(dynamic user)
        {

            var securitystamptype = new ClaimsIdentityOptions().SecurityStampClaimType;

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.GivenName, user.fullname),
                new Claim(ClaimTypes.Gender, user.gender.tostring()),
                new Claim(securitystamptype,user.securitystamp.tostring()),
                new Claim(ClaimTypes.NameIdentifier,user.id.tostring())
            };
            return claims;
        }
    }
}
