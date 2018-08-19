namespace AuthMiddlewareExample.Middleware
{
    using System;
    using System.Threading.Tasks;
    using Example.Services.Interfaces.TokenValidation;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthenticationMiddleware> _logger;
        private readonly ITokenValidator _tokenValidator;

        public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger, ITokenValidator tokenValidator)
        {
            _next = next;
            _logger = logger;
            _tokenValidator = tokenValidator;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                _logger.LogInformation("Checking Authenticartion");

                var authHeader = GetAuthHeader(context);

                if (authHeader == null)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }

                _logger.LogInformation("Checking if Header is Valid");

                var isValid = _tokenValidator.Validate(authHeader);

                if (isValid)
                {
                    await _next.Invoke(context).ConfigureAwait(false);
                    return;
                }

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"AuthMiddleware has exceptioned with the following message: ${e.Message}");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return;
            }
        }

        private string GetAuthHeader(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrWhiteSpace(authHeader))
            {
                _logger.LogWarning("No Auth Header");
                return null;
            }

            if (!authHeader.StartsWith("Bearer "))
            {
                _logger.LogWarning("Auth Header does not start with bearer");
                return null;
            }

            return authHeader.Substring(("Bearer ").Length).Trim();
        }
    }
}