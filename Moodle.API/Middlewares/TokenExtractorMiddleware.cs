using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Moodle.API.Middlewares
{
    public class TokenExtractorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public TokenExtractorMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            
            // Extract access token from Authorization header
            string accessToken = "";
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                var authorizationHeader = context.Request.Headers["Authorization"];
                var parts = authorizationHeader.FirstOrDefault().Split(' ');
                if (parts.Length == 2 && parts[0] == "Bearer")
                {
                    accessToken = parts[1];
                    //await Console.Out.WriteLineAsync();
                }
            }
            
            // Validate the access token
            if (!string.IsNullOrEmpty(accessToken))
            {
                try
                {
                    ValidateToken(accessToken);
                }
                catch (SecurityTokenException ex)
                {
                    Console.WriteLine($"Security token validation error: {ex.Message}");
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized; // Set unauthorized status code
                    context.Response.Headers.Add("Token-Expired", "true");
                    return;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Invalid token format: {ex.Message}");
                    // Decide how to handle invalid format based on your security needs
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized; // Example: Set unauthorized status code
                    return;
                }
            }

            // Call the next middleware in the pipeline
            await _next.Invoke(context);
        }

        private void ValidateToken(string accessToken)
        {
            var tokenValidationParameters = GetTokenValidationParameters();

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadJwtToken(accessToken);

            if (securityToken.ValidTo < DateTime.UtcNow)
            {
                throw new SecurityTokenException("Access token is expired.");
            }

            tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken validatedToken);
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {
            var issuerSigningKey = _configuration["Jwt:SecretKey"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey)),
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateLifetime = true
            };

            return tokenValidationParameters;
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class TokenExtractorMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenExtractorMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenExtractorMiddleware>(builder.ApplicationServices.GetService<IConfiguration>());
        }
    }
}
