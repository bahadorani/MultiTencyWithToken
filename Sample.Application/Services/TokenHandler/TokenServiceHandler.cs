using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Sample.Domain.ViewModels;
using Sample.Common;

namespace Sample.Application.Services.Handler
{
    public class TokenServiceHandler : ITokenServiceHandle
    {
        private readonly IOptions<TokenOptions> _tokenOptions;

        public TokenServiceHandler(IOptions<TokenOptions> tokenOptions)
        {
            _tokenOptions = tokenOptions;
        }

        public async Task<string> GenerateTokenAsync(TokenRequest model)
        {
            var now = DateTime.Now;

            var jwtToken = new JwtSecurityToken(
                issuer: _tokenOptions.Value.TokenIssuer,
                audience: _tokenOptions.Value.TokenAudience,
                claims: await GetTokenClaims(model),
                notBefore: now,
                expires: now.Add(_tokenOptions.Value.TokenExpiration),
                signingCredentials: _tokenOptions.Value.SigningCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return token;
        }

        public int? ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, _tokenOptions.Value.Parameters, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }

        private async Task<List<Claim>> GetTokenClaims(TokenRequest model)
        {
            return new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Iss, _tokenOptions.Value.TokenIssuer),
                new Claim(JwtRegisteredClaimNames.Jti, await _tokenOptions.Value.NonceGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat,
                new DateTimeOffset(DateTime.Now).ToUniversalTime().ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.NameIdentifier,model.UserId),
                new Claim(ClaimTypes.Name, model.UserName),
                new Claim("Tenancy",Constants.CurrentTenant),
            };
        }
        private Token GenerateTokenModel(TokenRequest model, string tokenString)
        {
            var token = new Token
            {
                UserName = model.UserName,
                Value = tokenString,
                CreatedDate = DateTime.Now,
                ExpireDate = DateTime.Now.Add(_tokenOptions.Value.TokenExpiration),
                UserId = model.UserId,
            };

            return token;
        }
    }
}
