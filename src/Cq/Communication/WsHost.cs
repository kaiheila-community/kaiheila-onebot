using System;
using System.Composition;
using System.Threading.Tasks;
using Fleck;
using Kaiheila.Cqhttp.Cq.Handlers;
using Kaiheila.Cqhttp.Storage;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

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
        /// <param name="cqActionHandler">CQHTTP任务处理器。</param>
        /// <param name="configHelper">提供访问应用配置能力的帮助类型。</param>
        public WsHost(
            ILogger<WsHost> logger,
            CqActionHandler cqActionHandler,
            ConfigHelper configHelper)
        {
            _logger = logger;
            _cqActionHandler = cqActionHandler;
            _configHelper = configHelper;
        }

        /// <summary>
        /// 启动CQHTTP WS主机。
        /// </summary>
        public void Run()
        {
            _logger.LogInformation("初始化CQHTTP WS主机。");

            _server = new WebSocketServer(
                $"ws://{_configHelper.Config.CqConfig.CqWsHostConfig.Host}:{_configHelper.Config.CqConfig.CqWsHostConfig.Port}");

            _server.Start(socket => socket.OnMessage = async s => await SocketOnMessage(socket, s));

            _logger.LogInformation(
                $"CQHTTP WS主机已经开始在ws://{_configHelper.Config.CqConfig.CqWsHostConfig.Host}:{_configHelper.Config.CqConfig.CqWsHostConfig.Port}上监听。");
        }

        private async Task SocketOnMessage(IWebSocketConnection socket, string raw)
        {
            JObject json;
            JToken echo;
            string action;
            JToken payload;

            try
            {
                json = JObject.Parse(raw);
                echo = json["echo"];
                // ReSharper disable once PossibleNullReferenceException
                action = json["action"].ToObject<string>();
                payload = json["params"];
            }
            catch (Exception)
            {
                await socket.Send("{\"status\": \"failed\",\"retcode\": 1400,\"data\": null}");
                return;
            }

            JToken token = _cqActionHandler.Process(action, payload);
            token["echo"] = echo;
            await socket.Send(token.ToString());
        }

        /// <summary>
        /// 释放CQHTTP WS主机和连接资源。
        /// </summary>
        public void Dispose()
        {
            _server.Dispose();
        }

        #region WebSocket

        private WebSocketServer _server;

        #endregion

        /// <summary>
        /// CQHTTP WS主机日志记录器。
        /// </summary>
        private readonly ILogger<WsHost> _logger;

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
