﻿using System;
using System.Collections.Generic;
using System.Linq;
using Kaiheila.Cqhttp.Cq.Controllers;
using Kaiheila.Cqhttp.Storage;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Logging;

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
        public void Process(string action, string payload)
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
            CqActionHandler cqActionHandler,
            ConfigHelper configHelper)
        {
            return listenOptions;
        }
    }
}
