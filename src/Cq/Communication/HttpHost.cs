using System;
using System.Net;
using Kaiheila.Cqhttp.Storage;
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
        /// <param name="configHelper">提供访问应用配置能力的帮助类型。</param>
        public HttpHost(
            ILogger<HttpHost> logger,
            ConfigHelper configHelper)
        {
            _logger = logger;

            _logger.LogInformation("初始化CQHTTP HTTP主机。");

            _configHelper = configHelper;
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
                    options.Listen(
                        IPAddress.Parse(_configHelper.Config.CqConfig.CqHttpHostConfig.Host),
                        5000,
                        listenOptions => {});
                });

        #endregion

        /// <summary>
        /// CQHTTP HTTP主机日志记录器。
        /// </summary>
        private readonly ILogger<HttpHost> _logger;

        /// <summary>
        /// 提供访问应用配置能力的帮助类型。
        /// </summary>
        private readonly ConfigHelper _configHelper;
    }
}
