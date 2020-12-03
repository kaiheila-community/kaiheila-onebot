using System;
using System.Collections.Generic;
using System.Composition;
using System.Threading.Tasks;
using Fleck;
using Kaiheila.OneBot.Cq.Events;
using Kaiheila.OneBot.Cq.Handlers;
using Kaiheila.OneBot.Storage;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Kaiheila.OneBot.Cq.Communication
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
        /// <param name="cqEventHandler">CQHTTP事件处理器。</param>
        /// <param name="configHelper">提供访问应用配置能力的帮助类型。</param>
        public WsHost(
            ILogger<WsHost> logger,
            CqActionHandler cqActionHandler,
            CqEventHandler cqEventHandler,
            ConfigHelper configHelper)
        {
            _logger = logger;
            _cqActionHandler = cqActionHandler;
            _cqEventHandler = cqEventHandler;
            _configHelper = configHelper;
        }

        /// <summary>
        /// 启动CQHTTP WS主机。
        /// </summary>
        public void Run()
        {
            _logger.LogInformation("初始化CQHTTP WS主机。");

            _server = new WebSocketServer(
                $"ws://{_configHelper.Config.CqConfig.CqWsHostConfig.Host}:{_configHelper.Config.CqConfig.CqWsHostConfig.Port}")
            {
                RestartAfterListenError = true
            };

            _server.Start(socket =>
            {
                _sockets.Add(socket);
                _logger.LogInformation($"WebSocket客户端接入。现在的客户端数量：{_sockets.Count}");
                socket.OnMessage = async s => await SocketOnMessage(socket, s);
                socket.OnClose = () =>
                {
                    if (_sockets.Contains(socket)) _sockets.Remove(socket);
                    _logger.LogInformation($"WebSocket客户端关闭。现在的客户端数量：{_sockets.Count}");
                };
            });

            _cqEventHandler.Event.Subscribe(OnCqEvent);

            _logger.LogInformation(
                $"CQHTTP WS主机已经开始在ws://{_configHelper.Config.CqConfig.CqWsHostConfig.Host}:{_configHelper.Config.CqConfig.CqWsHostConfig.Port}上监听。");
        }

        #region Event

        private void OnCqEvent(CqEventBase obj) => _sockets.ForEach(x => x.Send(obj.Result.ToString()));

        #endregion

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
            _server?.Dispose();
        }

        #region WebSocket

        private WebSocketServer _server;

        private List<IWebSocketConnection> _sockets = new List<IWebSocketConnection>();

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
        /// CQHTTP事件处理器。
        /// </summary>
        private readonly CqEventHandler _cqEventHandler;

        /// <summary>
        /// 提供访问应用配置能力的帮助类型。
        /// </summary>
        private readonly ConfigHelper _configHelper;
    }
}
