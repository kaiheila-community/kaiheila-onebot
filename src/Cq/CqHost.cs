using System.Composition;
using Kaiheila.Cqhttp.Cq.Communication;
using Kaiheila.Cqhttp.Storage;

namespace Kaiheila.Cqhttp.Cq
{
    /// <summary>
    /// CQHTTP主机，负责OneBot协议的接口监听和事件处理。
    /// </summary>
    [Export]
    public sealed class CqHost
    {
        /// <summary>
        /// 初始化CQHTTP主机。
        /// </summary>
        /// <param name="configHelper">提供访问应用配置能力的帮助类型。</param>
        /// <param name="httpHost">CQHTTP HTTP主机。</param>
        /// <param name="wsHost">CQHTTP WS主机。</param>
        public CqHost(
            ConfigHelper configHelper,
            HttpHost httpHost,
            WsHost wsHost)
        {
            _configHelper = configHelper;

            _httpHost = httpHost;
            _wsHost = wsHost;

            if (configHelper.Config.CqConfig.CqHttpHostConfig.Enable) _httpHost.Run();
            if (configHelper.Config.CqConfig.CqWsHostConfig.Enable) _wsHost.Run();
        }

        #region Communication Hosts

        /// <summary>
        /// CQHTTP HTTP主机。
        /// </summary>
        private readonly HttpHost _httpHost;

        /// <summary>
        /// CQHTTP WS主机。
        /// </summary>
        private readonly WsHost _wsHost;

        #endregion

        /// <summary>
        /// 提供访问应用配置能力的帮助类型。
        /// </summary>
        private readonly ConfigHelper _configHelper;
    }
}
