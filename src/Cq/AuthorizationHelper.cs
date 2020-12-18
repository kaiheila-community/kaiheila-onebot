using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Kaiheila.OneBot.Storage;
using Kaiheila.OneBot.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Kaiheila.OneBot.Cq
{
    public class AuthorizationMiddleware
    {
        private const string AuthorizationHeader = "Authorization";
        private const string BearerPrefix = "Bearer ";

        public AuthorizationMiddleware(
            RequestDelegate next,
            IOptions<ConfigHelper> options)
        {
            _next = next;
            _options = options;
        }

        private RequestDelegate _next;
        private readonly IOptions<ConfigHelper> _options;

        public async Task InvokeAsync(HttpContext context)
        {
            // Skip Authorization When IsNullOrEmpty
            if (string.IsNullOrEmpty(_options.Value.Config.CqConfig.CqAuthConfig.AccessToken))
            {
                await _next(context);
                return;
            }

            bool hasAccessTokenInHeader = context.Request.Headers
                .TryGetValue(AuthorizationHeader, out StringValues authValue) && authValue.Any();

            if (hasAccessTokenInHeader)
            {
                if (
                    authValue.FirstOrDefault() ==
                    BearerPrefix + _options.Value.Config.CqConfig.CqAuthConfig.AccessToken)
                {
                    await _next(context);
                    return;
                }

                context.Response.SetStatusCode(HttpStatusCode.Forbidden);
                return;
            }

            bool hasAccessTokenInQuery =
                context.Request.Query.TryGetValue("access_token", out authValue) &&
                authValue.Any();

            if (hasAccessTokenInQuery)
            {
                if (
                    authValue.FirstOrDefault() ==
                    _options.Value.Config.CqConfig.CqAuthConfig.AccessToken)
                {
                    await _next(context);
                    return;
                }

                context.Response.SetStatusCode(HttpStatusCode.Forbidden);
                return;
            }

            context.Response.SetStatusCode(HttpStatusCode.Unauthorized);
        }
    }

    public static class AuthorizationExtensions
    {
        public static IApplicationBuilder UseCqAuthorization(
            this IApplicationBuilder builder,
            ConfigHelper configHelper)
        {
            builder.UseMiddleware<AuthorizationMiddleware>(
                Options.Create(configHelper));
            return builder;
        }
    }
}
