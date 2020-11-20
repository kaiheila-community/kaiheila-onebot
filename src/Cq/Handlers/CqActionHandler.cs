using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Kaiheila.Cqhttp.Cq.Controllers;
using Kaiheila.Cqhttp.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Kaiheila.Cqhttp.Cq.Handlers
{
    /// <summary>
    /// CQHTTP任务处理器。
    /// </summary>
    public class CqActionHandler
    {
        /// <summary>
        /// 初始化CQHTTP任务处理器。
        /// </summary>
        /// <param name="logger"></param>
        public CqActionHandler(
            ILogger<CqActionHandler> logger)
        {
            _logger = logger;

            foreach (Type type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(x =>
                x.GetTypes()
                    .Where(type => Attribute.GetCustomAttribute(type, typeof(CqControllerAttribute)) is not null)))
            {
                string action = (Attribute.GetCustomAttribute(type, typeof(CqControllerAttribute)) as CqControllerAttribute)?.Action;
                if (action != null && !_controllers.ContainsKey(action)) _controllers.Add(action, type);
            }

            _logger.LogInformation($"加载了{_controllers.Count}个CQHTTP任务控制器。");
        }

        #region Process

        /// <summary>
        /// 执行任务。
        /// </summary>
        /// <param name="action">要执行的任务。</param>
        /// <param name="payload">JSON报文。</param>
        public void Process(string action, JObject payload)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Controllers

        /// <summary>
        /// CQHTTP任务控制器。
        /// </summary>
        private readonly Dictionary<string, Type> _controllers;

        #endregion

        /// <summary>
        /// CQHTTP任务处理器日志记录器。
        /// </summary>
        private readonly ILogger<CqActionHandler> _logger;
    }

    public static class CqActionHandlerHelper
    {
        public static ListenOptions UseCqActionHandler(
            this ListenOptions listenOptions,
            CqActionHandler cqActionHandler)
        {
            listenOptions.Use(
                next => async context =>
                {
                    HttpContext httpContext = context.GetHttpContext();

                    JObject payload;

                    switch (httpContext.Request.ContentType)
                    {
                        case "application/json":
                            try
                            {
                                payload = JObject.Parse(await new StreamReader(httpContext.Request.Body).ReadToEndAsync());
                            }
                            catch (Exception e)
                            {
                                return;
                            }
                            break;
                        default:
                            httpContext.Response.SetStatusCode(HttpStatusCode.NotAcceptable);
                            return;
                    }

                    try
                    {
                        cqActionHandler.Process(httpContext.Request.Path, payload);
                    }
                    catch (Exception e)
                    {
                        return;
                    }
                });

            return listenOptions;
        }
    }
}
