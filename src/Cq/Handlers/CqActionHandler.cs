﻿using System;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Kaiheila.OneBot.Cq.Controllers;
using Kaiheila.OneBot.Kh;
using Kaiheila.OneBot.Storage;
using Kaiheila.OneBot.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Kaiheila.OneBot.Cq.Handlers
{
    /// <summary>
    /// CQHTTP任务处理器。
    /// </summary>
    [Export]
    public class CqActionHandler
    {
        /// <summary>
        /// 初始化CQHTTP任务处理器。
        /// </summary>
        /// <param name="khHost">Kaiheila主机。</param>
        /// <param name="cqContext">CQHTTP上下文。</param>
        /// <param name="logger">CQHTTP任务处理器日志记录器。</param>
        /// <param name="configHelper">提供访问应用配置能力的帮助类型。</param>
        public CqActionHandler(
            KhHost khHost,
            CqContext cqContext,
            ILogger<CqActionHandler> logger,
            ConfigHelper configHelper)
        {
            _khHost = khHost;
            _cqContext = cqContext;
            _logger = logger;
            _configHelper = configHelper;

            foreach (Type type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(x =>
                x.GetTypes()
                    .Where(type => Attribute.GetCustomAttribute(type, typeof(CqControllerAttribute)) is not null)))
            {
                string action = (Attribute.GetCustomAttribute(type, typeof(CqControllerAttribute)) as CqControllerAttribute)?.Action;
                if (action == null || _controllers.ContainsKey(action) || type.FullName == null) continue;

                _controllers.Add(action, Activator.CreateInstance(type, _cqContext) as CqControllerBase);
            }

            _logger.LogInformation($"加载了{_controllers.Count}个CQHTTP任务控制器。");
        }

        #region Process

        /// <summary>
        /// 执行任务。
        /// </summary>
        /// <param name="action">要执行的任务。</param>
        /// <param name="payload">JSON报文。</param>
        public JToken Process(string action, JToken payload)
        {
            if (action.EndsWith("_async"))
            {
                action = action.Replace("_async", "");
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        if (!_controllers.TryGetValue(action, out CqControllerBase ctrl))
                        {
                            // TODO: Log
                            return;
                        }
                        
                        ctrl.Process(payload);
                    }
                    catch (CqControllerException e)
                    {
                        // TODO: Log
                    }
                });

                return JToken.FromObject(new
                {
                    status = "async",
                    retcode = 1
                });
            }

            if (!_controllers.TryGetValue(action, out CqControllerBase controller))
                throw new HttpRequestException(null, null, HttpStatusCode.NotFound);

            try
            {
                return JToken.FromObject(new
                {
                    status = "ok",
                    retcode = 0,
                    data = controller.Process(payload)
                });
            }
            catch (CqControllerException e)
            {
                // TODO: Log

                return JToken.FromObject(new
                {
                    status = "failed",
                    retcode = e.RetCode
                });
            }
        }

        #endregion

        #region Controllers

        /// <summary>
        /// CQHTTP任务控制器。
        /// </summary>
        private readonly Dictionary<string, CqControllerBase> _controllers = new Dictionary<string, CqControllerBase>();

        #endregion

        /// <summary>
        /// CQHTTP上下文。
        /// </summary>
        private readonly CqContext _cqContext;

        /// <summary>
        /// Kaiheila主机。
        /// </summary>
        private readonly KhHost _khHost;

        /// <summary>
        /// CQHTTP任务处理器日志记录器。
        /// </summary>
        private readonly ILogger<CqActionHandler> _logger;

        /// <summary>
        /// 提供访问应用配置能力的帮助类型。
        /// </summary>
        private readonly ConfigHelper _configHelper;
    }

    public class CqActionHandlerMiddleware
    {
        public CqActionHandlerMiddleware(
            RequestDelegate next,
            IOptions<CqActionHandler> options)
        {
            _next = next;
            _options = options;
        }

        private RequestDelegate _next;
        private readonly IOptions<CqActionHandler> _options;

        public async Task InvokeAsync(HttpContext context)
        {
            JObject payload;

            switch (context.Request.ContentType)
            {
                case "application/json":
                    try
                    {
                        payload = JObject.Parse(await new StreamReader(context.Request.Body).ReadToEndAsync());
                    }
                    catch (Exception)
                    {
                        context.Response.SetStatusCode(HttpStatusCode.BadRequest);
                        return;
                    }
                    break;
                default:
                    context.Response.SetStatusCode(HttpStatusCode.NotAcceptable);
                    return;
            }

            if (string.IsNullOrEmpty(context.Request.Path.Value))
            {
                context.Response.SetStatusCode(HttpStatusCode.BadRequest);
                return;
            }

            try
            {
                JToken token = _options.Value.Process(context.Request.Path.Value.Trim('/'), payload);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(token.ToString());
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode != null) context.Response.SetStatusCode((HttpStatusCode)e.StatusCode);
            }
            catch (Exception)
            {
                // Ignore
            }
        }
    }

    public static class CqActionHandlerExtensions
    {
        public static IApplicationBuilder UseCqActionHandler(
            this IApplicationBuilder builder,
            CqActionHandler cqActionHandler)
        {
            builder.UseMiddleware<CqActionHandlerMiddleware>(
                Options.Create(cqActionHandler));
            return builder;
        }
    }

    [Serializable]
    public class CqControllerException : Exception
    {
        public CqControllerException(
            int retCode = 100,
            string message = "",
            Exception inner = null) : base(message, inner)
        {
            RetCode = retCode;
        }

        public readonly int RetCode;
    }
}
