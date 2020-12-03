using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Kaiheila.Events;
using Kaiheila.OneBot.Cq.Events;
using Kaiheila.OneBot.Kh;
using Kaiheila.OneBot.Storage;
using Microsoft.Extensions.Logging;

namespace Kaiheila.OneBot.Cq.Handlers
{
    /// <summary>
    /// CQHTTP事件处理器。
    /// </summary>
    [Export]
    public class CqEventHandler
    {
        /// <summary>
        /// 初始化CQHTTP事件处理器。
        /// </summary>
        /// <param name="khHost">Kaiheila主机。</param>
        /// <param name="cqContext">CQHTTP上下文。</param>
        /// <param name="logger">CQHTTP事件处理器日志记录器。</param>
        /// <param name="configHelper">提供访问应用配置能力的帮助类型。</param>
        public CqEventHandler(
            KhHost khHost,
            CqContext cqContext,
            ILogger<CqActionHandler> logger,
            ConfigHelper configHelper)
        {
            _khHost = khHost;
            _cqContext = cqContext;
            _logger = logger;
            _configHelper = configHelper;

            _logger.LogInformation("初始化CQHTTP事件处理器。");

            foreach (Type type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(x =>
                x.GetTypes()
                    .Where(type => Attribute.GetCustomAttribute(type, typeof(CqEventAttribute)) is not null)))
            {
                Type khEventType = (Attribute.GetCustomAttribute(type, typeof(CqEventAttribute)) as CqEventAttribute)?.Type;
                if (khEventType == null || _eventTypes.ContainsKey(khEventType) || type.FullName == null) continue;

                _eventTypes.Add(khEventType, type);
            }

            khHost.Bot.Event.Select(Process).Subscribe(Event);

            _logger.LogInformation($"加载了{_eventTypes.Count}个CQHTTP事件类型。");
        }

        #region Event

        private CqEventBase Process(KhEventBase khEvent)
        {
            if (!_eventTypes.TryGetValue(khEvent.GetType(), out Type eventType))
                return null;

            CqEventBase cqEvent = Activator.CreateInstance(eventType, _cqContext, khEvent) as CqEventBase;
            return cqEvent;
        }

        public Subject<CqEventBase> Event = new Subject<CqEventBase>();

        #endregion

        #region Event Types

        private readonly Dictionary<Type, Type> _eventTypes = new Dictionary<Type, Type>();

        #endregion

        /// <summary>
        /// Kaiheila主机。
        /// </summary>
        private readonly KhHost _khHost;

        /// <summary>
        /// CQHTTP上下文。
        /// </summary>
        private readonly CqContext _cqContext;

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
