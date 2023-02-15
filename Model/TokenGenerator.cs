using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace SignInWithApple.Model
{
    public class TokenGenerator
    {
        public static string CreateNewToken()
        {
            const string iss = "62QM29578N"; // your accounts team ID found in the dev portal
            const string aud = "https://appleid.apple.com";
            const string sub = "com.scottbrady91.authdemo.service"; // same as client_id
            var now = DateTime.UtcNow;

            const string privateKey = "MIGTAgEAMBMGByqGSM49AgEGCCqGSM49AwEHBHkwdwIBAQQgnbfHJQO9feC7yKOenScNctvHUP+Hp3AdOKnjUC3Ee9GgCgYIKoZIzj0DAQehRANCAATMgckuqQ1MhKALhLT/CA9lZrLA+VqTW/iIJ9GKimtC2GP02hCc5Vac8WuN6YjynF3JPWKTYjg2zqex5Sdn9Wj+";
            var ecdsa = ECDsa.Create();
            ecdsa?.ImportPkcs8PrivateKey(Convert.FromBase64String(privateKey), out _);

            var handler = new JsonWebTokenHandler();
            return handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = iss,
                Audience = aud,
                Claims = new Dictionary<string, object> { { "sub", sub } },
                Expires = now.AddMinutes(5), // expiry can be a maximum of 6 months - generate one per request or re-use until expiration
                IssuedAt = now,
                NotBefore = now,
                SigningCredentials = new SigningCredentials(new ECDsaSecurityKey(ecdsa), SecurityAlgorithms.EcdsaSha256)
            });
        }
    }
}
