using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Kaiheila.Cqhttp.Cq.Communication
{
    /// <summary>
    /// CQHTTP HTTP主机，负责OneBot协议中HTTP接口的监听和事件处理。
    /// </summary>
    public class HttpHost : IDisposable
    {
        /// <summary>
        /// 初始化CQHTTP HTTP主机。
        /// </summary>
        /// <param name="logger">CQHTTP HTTP主机日志记录器。</param>
        public HttpHost(ILogger<HttpHost> logger)
        {
            _logger = logger;

            _logger.LogInformation("初始化CQHTTP HTTP主机。");

            _webHost = CreateWebHostBuilder().Build();
        }

        /// <summary>
        /// 释放CQHTTP HTTP主机和连接资源。
        /// </summary>
        public void Dispose()
        {
            _webHost.Dispose();
        }

        #region Web Host

        private readonly IWebHost _webHost;

        private IWebHostBuilder CreateWebHostBuilder() =>
            new WebHostBuilder().UseKestrel(options =>
                {
                })
                .Configure(builder =>
                {
                });

        #endregion

        /// <summary>
        /// CQHTTP HTTP主机日志记录器。
        /// </summary>
        private readonly ILogger<HttpHost> _logger;
    }
}
