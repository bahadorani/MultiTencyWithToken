using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Sample.Domain.ViewModels
{
    public class TokenOptions
    {
        public int HoursToExpire { get; set; }
        public TimeSpan TokenExpiration => Expiration;
        public string TokenIssuer => Issuer;
        public string TokenAudience => Audience;
        
        private static string Audience => "CJAudience";
        private static string Issuer => "CJMultiTenancyApp";
        private static string SecretKey => "tXKQpGR2wxsT9np7iETYDsHaJ1y7G1aYlGGvQKfawxfeCwnjAm6oW0TnUFSpIK8X";
        private static string Algorithm => SecurityAlgorithms.HmacSha512Signature;
        private static SymmetricSecurityKey SigningKey => new(Encoding.ASCII.GetBytes(SecretKey));
        private TimeSpan Expiration => TimeSpan.FromHours(HoursToExpire+1);
        public Func<Task<string>> NonceGenerator => () => Task.FromResult(Guid.NewGuid().ToString());
        public SigningCredentials SigningCredentials => new(SigningKey, Algorithm);

        public string LoginPath => "/account/gettoken";
        public string CaptchaLoginPath => "/";

        public readonly TokenValidationParameters Parameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SigningKey,
            ValidateIssuer = true,
            ValidIssuer = Issuer,
            ValidateAudience = true,
            ValidAudience = Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    }
}