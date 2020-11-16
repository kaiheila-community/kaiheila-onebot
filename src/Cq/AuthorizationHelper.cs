using System.Linq;
using Kaiheila.Cqhttp.Storage;
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

                    if (
                        context.GetHttpContext().Request.Headers
                            .TryGetValue(AuthorizationHeader, out StringValues authValue) &&
                        authValue.Any() &&
                        authValue.FirstOrDefault() ==
                        BearerPrefix + configHelper.Config.CqConfig.CqAuthConfig.AccessToken)
                        await next(context);
                });

            return listenOptions;
        }
    }
}
