using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using Kaiheila.Cqhttp.Cq.Controllers;
using Kaiheila.Cqhttp.Storage;
using Kaiheila.Cqhttp.Utils;
using Microsoft.AspNetCore.Builder;
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
        /// <param name="logger">CQHTTP任务处理器日志记录器。</param>
        /// <param name="configHelper">提供访问应用配置能力的帮助类型。</param>
        public CqActionHandler(
            ILogger<CqActionHandler> logger,
            ConfigHelper configHelper)
        {
            _logger = logger;
            _configHelper = configHelper;

            foreach (Type type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(x =>
                x.GetTypes()
                    .Where(type => Attribute.GetCustomAttribute(type, typeof(CqControllerAttribute)) is not null)))
            {
                string action = (Attribute.GetCustomAttribute(type, typeof(CqControllerAttribute)) as CqControllerAttribute)?.Action;
                if (action == null || _controllers.ContainsKey(action) || type.FullName == null) continue;

                object[] parameters = {new CqControllerContext {ConfigHelper = _configHelper}};

                _controllers.Add(action,
                    Assembly.GetExecutingAssembly().CreateInstance(
                        type.FullName,
                        false,
                        BindingFlags.Default,
                        null,
                        parameters,
                        null,
                        null
                    ) as CqControllerBase);
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
            if (!_controllers.TryGetValue(action, out CqControllerBase controller))
                throw new HttpRequestException(null, null, HttpStatusCode.NotFound);

            controller.Process(payload);
        }

        #endregion

        #region Controllers

        /// <summary>
        /// CQHTTP任务控制器。
        /// </summary>
        private readonly Dictionary<string, CqControllerBase> _controllers = new Dictionary<string, CqControllerBase>();

        #endregion

        /// <summary>
        /// CQHTTP任务处理器日志记录器。
        /// </summary>
        private readonly ILogger<CqActionHandler> _logger;

        /// <summary>
        /// 提供访问应用配置能力的帮助类型。
        /// </summary>
        private readonly ConfigHelper _configHelper;
    }

    public static class CqActionHandlerHelper
    {
        public static IApplicationBuilder UseCqActionHandler(
            this IApplicationBuilder builder,
            CqActionHandler cqActionHandler)
        {
            builder.Use(
                next => async context =>
                {
                    JObject payload;

                    switch (context.Request.ContentType)
                    {
                        case "application/json":
                            try
                            {
                                payload = JObject.Parse(await new StreamReader(context.Request.Body).ReadToEndAsync());
                            }
                            catch (Exception e)
                            {
                                return;
                            }
                            break;
                        default:
                            context.Response.SetStatusCode(HttpStatusCode.NotAcceptable);
                            return;
                    }

                    try
                    {
                        cqActionHandler.Process(context.Request.Path.Value.Trim('/'), payload);
                    }
                    catch (HttpRequestException e)
                    {
                        if (e.StatusCode != null) context.Response.SetStatusCode((HttpStatusCode) e.StatusCode);
                        return;
                    }
                    catch (Exception e)
                    {
                        return;
                    }
                });

            return builder;
        }
    }
}
