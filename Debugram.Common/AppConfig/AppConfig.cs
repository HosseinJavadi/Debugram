using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugram.Common.AppConfig
{
    public class AppConfig
    {
        public JwtSetting JwtSetting { get; set; }
        public ElmahSetting Elmah { get; set; }
    }
    public class JwtSetting
    {
        public string SecretKey { get; set; }
        public bool RequireSignedTokens { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public bool RequireExpirationTime { get; set; }
        public bool ValidateLifetime  { get; set; }

        public bool ValidateAudience { get; set; }
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool SaveToken { get; set; }
        public bool RequireHttpsMetadata { get; set; }
        public int Expires { get; set; }
        public int NotBefore { get; set; }
        public string EncryptKey { get; set; }

    }
    public class ElmahSetting
    {
        public string Key { get; set; }
        public string ElmahConnectionString { get; set; }
    }
}
