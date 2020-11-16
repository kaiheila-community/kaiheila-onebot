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
                next => context =>
                {
                    // Skip Authorization When IsNullOrEmpty
                    if (string.IsNullOrEmpty(configHelper.Config.CqConfig.CqAuthConfig.AccessToken))
                        return next(context);

                    if (
                        context.GetHttpContext().Request.Headers
                            .TryGetValue(AuthorizationHeader, out StringValues authValue) &&
                        authValue.FirstOrDefault() ==
                        BearerPrefix + configHelper.Config.CqConfig.CqAuthConfig.AccessToken)
                        return next(context);

                    return null;
                });

            return listenOptions;
        }
    }
}
