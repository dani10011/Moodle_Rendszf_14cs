using Microsoft.IdentityModel.Tokens;
using Moodle.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
            using (var scope = context.RequestServices.CreateScope()) // Create scoped service provider
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MoodleDbContext>();
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
                        await Console.Out.WriteLineAsync("Access token: " + accessToken);
                    }
                }

                string refreshToken = "";
                if (context.Request.Headers.ContainsKey("Refresh"))
                {
                    var authorizationHeader = context.Request.Headers["Refresh"];
                    var parts = authorizationHeader.FirstOrDefault().Split(' ');
                    if (parts.Length == 2 && parts[0] == "Refresh")
                    {
                        refreshToken = parts[1];
                        //await Console.Out.WriteLineAsync();
                        await Console.Out.WriteLineAsync("Refresh token: " + refreshToken);
                    }
                }

                // Validate the access token
                if (!string.IsNullOrEmpty(accessToken))
                {
                    try
                    {
                        ValidateToken(accessToken, refreshToken, context, dbContext);
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
            }
            // Call the next middleware in the pipeline
            await _next.Invoke(context);
        }

        private void ValidateToken(string accessToken, string refreshToken, HttpContext context, MoodleDbContext dbContext)
        {
            var tokenValidationParameters = GetTokenValidationParameters();

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadJwtToken(accessToken);
            var refreshT = tokenHandler.ReadJwtToken(refreshToken);

            if (securityToken.ValidTo < DateTime.UtcNow && refreshT.ValidTo < DateTime.UtcNow)
            {
                throw new SecurityTokenException("Access token is expired.");
            }
            else if (securityToken.ValidTo < DateTime.UtcNow && refreshT.ValidTo > DateTime.UtcNow)
            {
                (string NameIdentifier, string Role) = GetUserInfoFromClaims(GetPrincipalFromExpiredToken(accessToken));
                var aktualis_felhasznalo = dbContext.Users.SingleOrDefault(p => p.Id.ToString() == NameIdentifier);
                //var aktualis_felhasznalo = dbContext.Users.SingleOrDefault();
                Console.WriteLine();
                if (aktualis_felhasznalo != null)
                {
                    /*
                    Token token = new Token(_configuration);
                    string newtoken = token.GenerateAccessToken(aktualis_felhasznalo);
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    context.Response.Headers.Add("New-Token ", newtoken);
                    */
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    context.Response.Headers.Add("X-New-Token-Required", "true");
                }
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

        public (string NameIdentifier, string Role) GetUserInfoFromClaims(ClaimsPrincipal user)
        {
            if (user.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
            {
                var nameIdentifier = user.FindFirstValue(ClaimTypes.NameIdentifier);
                var role = user.FindFirstValue(ClaimTypes.Role);
                return (nameIdentifier, role);
            }

            throw new Exception("ClaimsPrincipal does not contain NameIdentifier or Role claims.");
        }


        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var Key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
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
