using System;
using System.Security.Cryptography;

namespace EffortlessApi.Core
{
    public class JwtSettings : IJwtSettings
    {
        public string SigningKey { get; private set; }
        public string Issuer { get; private set; }

        public JwtSettings(string signingKey, string issuer)
        {
            if (signingKey == null)
            {
                var hmac = new HMACSHA256();
                signingKey = Convert.ToBase64String(hmac.Key);
            }

            SigningKey = signingKey;
            Issuer = issuer;
        }
    }
}
