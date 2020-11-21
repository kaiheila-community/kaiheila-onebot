using System.Reactive.Subjects;
using Kaiheila.Cqhttp.Cq.Events;
using Kaiheila.Cqhttp.Storage;
using Microsoft.Extensions.Logging;

namespace Kaiheila.Cqhttp.Cq.Handlers
{
    /// <summary>
    /// CQHTTP事件处理器。
    /// </summary>
    public class CqEventHandler
    {
        /// <summary>
        /// 初始化CQHTTP事件处理器。
        /// </summary>
        /// <param name="logger">CQHTTP任务处理器日志记录器。</param>
        /// <param name="configHelper">提供访问应用配置能力的帮助类型。</param>
        public CqEventHandler(
            ILogger<CqActionHandler> logger,
            ConfigHelper configHelper)
        {
            _logger = logger;
            _configHelper = configHelper;
        }

        #region Event

        public Subject<CqEventBase> Event = new Subject<CqEventBase>();

        #endregion

        /// <summary>
        /// CQHTTP事件处理器日志记录器。
        /// </summary>
        private readonly ILogger<CqActionHandler> _logger;

        /// <summary>
        /// 提供访问应用配置能力的帮助类型。
        /// </summary>
        private readonly ConfigHelper _configHelper;
    }
}
