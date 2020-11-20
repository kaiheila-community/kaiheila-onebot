using System.Linq;
using System.Net;
using Kaiheila.Cqhttp.Storage;
using Kaiheila.Cqhttp.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Primitives;

namespace Kaiheila.Cqhttp.Cq
{
    public static class AuthorizationHelper
    {
        private const string AuthorizationHeader = "Authorization";
        private const string BearerPrefix = "Bearer ";

        public static ListenOptions UseCqAuthorization(
            this ListenOptions listenOptions,
            ConfigHelper configHelper)
        {
            listenOptions.Use(
                next => async context =>
                {
                    // Skip Authorization When IsNullOrEmpty
                    if (string.IsNullOrEmpty(configHelper.Config.CqConfig.CqAuthConfig.AccessToken))
                    {
                        await next(context);
                        return;
                    }

                    HttpContext httpContext = context.GetHttpContext();

                    bool hasAccessTokenInHeader = httpContext.Request.Headers
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

                        httpContext.Response.SetStatusCode(HttpStatusCode.Forbidden);
                        return;
                    }

                    bool hasAccessTokenInQuery =
                        httpContext.Request.Query.TryGetValue("access_token", out authValue) &&
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

                        httpContext.Response.SetStatusCode(HttpStatusCode.Forbidden);
                        return;
                    }

                    httpContext.Response.SetStatusCode(HttpStatusCode.Unauthorized);
                });

            return listenOptions;
        }
    }
}
