using System;
using System.Composition;
using Microsoft.Extensions.Logging;

namespace Kaiheila.Cqhttp.Cq.Communication
{
    /// <summary>
    /// CQHTTP WS主机，负责OneBot协议中WS接口的监听和事件处理。
    /// </summary>
    [Export]
    public class WsHost : IDisposable
    {
        /// <summary>
        /// 初始化CQHTTP WS主机。
        /// </summary>
        /// <param name="logger">CQHTTP WS主机日志记录器。</param>
        public WsHost(ILogger<WsHost> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 释放CQHTTP WS主机和连接资源。
        /// </summary>
        public void Dispose()
        {

        }

        /// <summary>
        /// CQHTTP WS主机日志记录器。
        /// </summary>
        private readonly ILogger<WsHost> _logger;
    }
}
