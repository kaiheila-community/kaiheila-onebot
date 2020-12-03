using System;
using System.Composition;
using System.Net;
using Kaiheila.OneBot.Cq.Handlers;
using Kaiheila.OneBot.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Kaiheila.OneBot.Cq.Communication
{
    /// <summary>
    /// CQHTTP HTTP主机，负责OneBot协议中HTTP接口的监听和事件处理。
    /// </summary>
    [Export]
    public class HttpHost : IDisposable
    {
        /// <summary>
        /// 初始化CQHTTP HTTP主机。
        /// </summary>
        /// <param name="logger">CQHTTP HTTP主机日志记录器。</param>
        /// <param name="cqActionHandler">CQHTTP任务处理器。</param>
        /// <param name="configHelper">提供访问应用配置能力的帮助类型。</param>
        public HttpHost(
            ILogger<HttpHost> logger,
            CqActionHandler cqActionHandler,
            ConfigHelper configHelper)
        {
            _logger = logger;
            _cqActionHandler = cqActionHandler;
            _configHelper = configHelper;
        }

        /// <summary>
        /// 启动CQHTTP HTTP主机。
        /// </summary>
        public void Run()
        {
            _logger.LogInformation("初始化CQHTTP HTTP主机。");

            _webHost = CreateWebHostBuilder().Build();

            _webHost.RunAsync();
            _logger.LogInformation(
                $"CQHTTP HTTP主机已经开始在http://{_configHelper.Config.CqConfig.CqHttpHostConfig.Host}:{_configHelper.Config.CqConfig.CqHttpHostConfig.Port}上监听。");
        }

        /// <summary>
        /// 释放CQHTTP HTTP主机和连接资源。
        /// </summary>
        public void Dispose()
        {
            _webHost?.Dispose();
        }

        #region Web Host

        private IWebHost _webHost;

        private IWebHostBuilder CreateWebHostBuilder() =>
            new WebHostBuilder().UseKestrel(options =>
                {
                    options.Listen(
                        IPAddress.Parse(_configHelper.Config.CqConfig.CqHttpHostConfig.Host),
                        _configHelper.Config.CqConfig.CqHttpHostConfig.Port
                        );
                })
                .Configure(builder =>
                {
                    builder
                        .UseCqAuthorization(_configHelper)
                        .UseCqActionHandler(_cqActionHandler);
                });

        #endregion

        /// <summary>
        /// CQHTTP HTTP主机日志记录器。
        /// </summary>
        private readonly ILogger<HttpHost> _logger;

        /// <summary>
        /// CQHTTP任务处理器。
        /// </summary>
        private readonly CqActionHandler _cqActionHandler;

        /// <summary>
        /// 提供访问应用配置能力的帮助类型。
        /// </summary>
        private readonly ConfigHelper _configHelper;
    }
}
