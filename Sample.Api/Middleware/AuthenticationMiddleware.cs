using Domino.Application;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sample.Application.Services.Handler;
using Sample.Common;
using Sample.Domain.Models;
using Sample.Domain.ViewModels;
using System.Reflection.Metadata;
using System.Text;

namespace Sample.Api.Middleware
{
    public class AuthenticationMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly JsonSerializerSettings _serializerSettings;
        private ITokenServiceHandle _tokenHandler;
        private IAccountService _accountService;
        private IOptions<TokenOptions> _tokenOptions;

        public int Order => 1;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(
            HttpContext context,
            ITokenServiceHandle tokenHandler,
            IAccountService accountService,
            IOptions<TokenOptions> tokenOptions)
        {
            _tokenHandler = tokenHandler;
            _accountService = accountService;
            _tokenOptions = tokenOptions;

            await GenerateToken(context);
            await _next(context);
        }

        private bool IsTokenRelatedRequest(HttpContext context)
        {
            var httpMethod = context.Request.Method;
            var path = context.Request.Path;

            if (string.IsNullOrWhiteSpace(path) || httpMethod != HttpMethods.Post) return false;
            if (context.Request.Path.Equals(_tokenOptions.Value.LoginPath, StringComparison.Ordinal)) return true;

            return false;
        }

        private bool IsResponseAlreadyBuilt(HttpContext context)
        {
            return context.Response.HasStarted;
        }

        private async Task GenerateToken(HttpContext context)
        {
            var requestBody = await ReadRequestBody(context);

            var tokenRequest = new TokenRequest()
            {
                UserId = "1",
                UserName = "Bahadorani",
                Tenant = Constants.CurrentTenant
            };

            var jwtToken = await _tokenHandler.GenerateTokenAsync(tokenRequest!);
            var token = new TokenViewModel
            {
                Token = jwtToken,
                Name = tokenRequest.UserName,
                Tenancy = tokenRequest.Tenant,
            };
            var tenant = new Tenant { Name = token.Tenancy, ConnectionString = $"Server=.;Database={token.Tenancy}Sample;Trusted_Connection=True;TrustServerCertificate=True;" };
            context.Items["CurrentTenant"] = tenant;
            //context.Response.ContentType = "application/json";
            //await context.Response.WriteAsync(JsonConvert.SerializeObject(token));
        }

        private async Task<LoginViewModel> ReadRequestBody(HttpContext context)
        {
            using var streamReader = new StreamReader(context.Request.Body, Encoding.UTF8);
            var jsonBody = await streamReader.ReadToEndAsync();
            var body = JsonConvert.DeserializeObject<LoginViewModel>(jsonBody)!;
            return body;
        }

    }
}
