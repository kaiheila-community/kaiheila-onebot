using System;
using System.Collections.Generic;
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
        }

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
}
