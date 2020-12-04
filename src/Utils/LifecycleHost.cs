using System;
using System.Composition;
using Kaiheila.OneBot.Cq;
using Microsoft.Extensions.Logging;

namespace Kaiheila.OneBot.Utils
{
    /// <summary>
    /// 生命周期主机。
    /// </summary>
    [Export]
    public sealed class LifecycleHost
    {
        /// <summary>
        /// 初始化生命周期主机。
        /// </summary>
        /// <param name="cqHost">CQHTTP主机。</param>
        /// <param name="logger">生命周期主机日志记录器。</param>
        public LifecycleHost(
            CqHost cqHost,
            ILogger<LifecycleHost> logger)
        {
            _logger = logger;

            AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) =>
                _logger.LogCritical(
                    eventArgs.ExceptionObject as Exception,
                    "出现异常。这个异常是由kaiheila-cqhttp引起的。");

            _cqHost = cqHost;
        }

        /// <summary>
        /// CQHTTP主机。
        /// </summary>
        private readonly CqHost _cqHost;

        /// <summary>
        /// 生命周期主机日志记录器。
        /// </summary>
        private readonly ILogger<LifecycleHost> _logger;
    }
}
