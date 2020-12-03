using System.Linq;
using System.Net;
using Kaiheila.OneBot.Storage;
using Kaiheila.OneBot.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Primitives;

namespace Kaiheila.OneBot.Cq
{
    public static class AuthorizationHelper
    {
        private const string AuthorizationHeader = "Authorization";
        private const string BearerPrefix = "Bearer ";

        public static IApplicationBuilder UseCqAuthorization(
            this IApplicationBuilder builder,
            ConfigHelper configHelper)
        {
            builder.Use(
                next => async context =>
                {
                    // Skip Authorization When IsNullOrEmpty
                    if (string.IsNullOrEmpty(configHelper.Config.CqConfig.CqAuthConfig.AccessToken))
                    {
                        await next(context);
                        return;
                    }

                    bool hasAccessTokenInHeader = context.Request.Headers
                        .TryGetValue(AuthorizationHeader, out StringValues authValue) && authValue.Any();

                    if (hasAccessTokenInHeader)
                    {
                        if (
                            authValue.FirstOrDefault() ==
                            BearerPrefix + configHelper.Config.CqConfig.CqAuthConfig.AccessToken)
                        {
                            await next(context);
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
                            configHelper.Config.CqConfig.CqAuthConfig.AccessToken)
                        {
                            await next(context);
                            return;
                        }

                        context.Response.SetStatusCode(HttpStatusCode.Forbidden);
                        return;
                    }

                    context.Response.SetStatusCode(HttpStatusCode.Unauthorized);
                });

            return builder;
        }
    }
}
