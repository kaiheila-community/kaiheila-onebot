using System;
using Kaiheila.Cqhttp.Cq.Communication;

namespace Kaiheila.Cqhttp.Cq
{
    /// <summary>
    /// CQHTTP主机，负责OneBot协议的接口监听和事件处理。
    /// </summary>
    public sealed class CqHost : IDisposable
    {
        /// <summary>
        /// 初始化CQHTTP主机。
        /// </summary>
        /// <param name="httpHost">CQHTTP HTTP主机。</param>
        public CqHost(
            HttpHost httpHost)
        {
            _httpHost = httpHost;
        }

        /// <summary>
        /// 释放CQHTTP主机和连接资源。
        /// </summary>
        public void Dispose()
        {

        }

        #region Communication Hosts

        /// <summary>
        /// CQHTTP HTTP主机。
        /// </summary>
        private readonly HttpHost _httpHost;

        #endregion
    }
}
