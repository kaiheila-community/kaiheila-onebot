using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kaiheila.OneBot.Storage
{
    /// <summary>
    /// 应用配置。
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Config
    {
        /// <summary>
        /// 指定兼容性模式。
        /// </summary>
        [JsonProperty("compatibility_mode")]
        public CompatibilityMode CompatibilityMode { get; set; } = CompatibilityMode.Off;

        /// <summary>
        /// Kaiheila配置。
        /// </summary>
        [JsonProperty("kaiheila")]
        public KhConfig KhConfig { get; set; } = new KhConfig();

        /// <summary>
        /// OneBot配置。
        /// </summary>
        [JsonProperty("onebot")]
        public CqConfig CqConfig { get; set; } = new CqConfig();
    }

    /// <summary>
    /// 兼容性模式。
    /// </summary>
    public enum CompatibilityMode
    {
        Off = 0,
        OneBot = 20,
        Strict = 30
    }

    /// <summary>
    /// Kaiheila配置。
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class KhConfig
    {
        /// <summary>
        /// 指定Kaiheila客户端模式。
        /// </summary>
        [JsonProperty("mode")]
        public KhClientMode KhClientMode { get; set; } = KhClientMode.WebHook;

        /// <summary>
        /// 指定Kaiheila消息发送模式。
        /// </summary>
        [JsonProperty("sending_mode")]
        public KhSendingMode KhSendingMode { get; set; } = KhSendingMode.Normal;

        /// <summary>
        /// Kaiheila V2客户端配置。
        /// </summary>
        [JsonProperty("v2")]
        public KhClientV2Config KhClientV2Config { get; set; } = new KhClientV2Config();

        /// <summary>
        /// Kaiheila鉴权配置。
        /// </summary>
        [JsonProperty("auth")]
        public KhAuthConfig KhAuthConfig { get; set; } = new KhAuthConfig();
    }

    /// <summary>
    /// Kaiheila客户端模式。
    /// </summary>
    public enum KhClientMode
    {
        WebHook = 0,
        WebSocket = 1,
        V2 = 10
    }

    /// <summary>
    /// Kaiheila消息发送模式。
    /// </summary>
    public enum KhSendingMode
    {
        Normal = 0,
        Plain = 10
    }

    /// <summary>
    /// Kaiheila V2客户端配置。
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class KhClientV2Config
    {
        /// <summary>
        /// WS客户端连接的IP。
        /// </summary>
        [JsonProperty("host")]
        public string Host { get; set; } = "127.0.0.1";

        /// <summary>
        /// WS客户端连接的端口。
        /// </summary>
        [JsonProperty("port")]
        public int Port { get; set; } = 7700;
    }

    /// <summary>
    /// Kaiheila鉴权配置。
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class KhAuthConfig
    {
        /// <summary>
        /// Kaiheila鉴权使用的Cookie中的auth字段。
        /// </summary>
        [JsonProperty("cookie_auth")]
        public string CookieAuth { get; set; } = "";
    }

    /// <summary>
    /// OneBot配置。
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class CqConfig
    {
        /// <summary>
        /// OneBot HTTP主机配置。
        /// </summary>
        [JsonProperty("http")]
        public CqHttpHostConfig CqHttpHostConfig { get; set; } = new CqHttpHostConfig();

        /// <summary>
        /// OneBot HTTP POST主机配置。
        /// </summary>
        [JsonProperty("http_post")]
        public CqHttpPostHostConfig CqHttpPostHostConfig { get; set; } = new CqHttpPostHostConfig();

        /// <summary>
        /// OneBot WS主机配置。
        /// </summary>
        [JsonProperty("ws")]
        public CqWsHostConfig CqWsHostConfig { get; set; } = new CqWsHostConfig();

        /// <summary>
        /// OneBot WS Reverse主机配置。
        /// </summary>
        [JsonProperty("ws_reverse")]
        public CqWsReverseHostConfig CqWsReverseHostConfig { get; set; } = new CqWsReverseHostConfig();

        /// <summary>
        /// OneBot 鉴权配置。
        /// </summary>
        [JsonProperty("auth")]
        public CqAuthConfig CqAuthConfig { get; set; } = new CqAuthConfig();
    }

    /// <summary>
    /// OneBot HTTP主机配置。
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class CqHttpHostConfig
    {
        /// <summary>
        /// 是否启用HTTP。
        /// </summary>
        [JsonProperty("enable")]
        public bool Enable { get; set; } = true;

        /// <summary>
        /// HTTP服务器监听的IP。
        /// </summary>
        [JsonProperty("host")]
        public string Host { get; set; } = "0.0.0.0";

        /// <summary>
        /// HTTP服务器监听的端口。
        /// </summary>
        [JsonProperty("port")]
        public int Port { get; set; } = 5700;
    }

    /// <summary>
    /// OneBot HTTP POST主机配置。
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class CqHttpPostHostConfig
    {
        /// <summary>
        /// HTTP POST事件上报 URL。
        /// </summary>
        /// <remarks>
        /// 键为Url，值为对应Url的Secret。
        /// </remarks>
        [JsonProperty("urls")]
        public Dictionary<string, string> UrlList { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 以秒为单位的HTTP POST超时时间。
        /// </summary>
        /// <remarks>
        /// 0表示不设置超时。
        /// </remarks>
        [JsonProperty("timeout")]
        public int Timeout { get; set; }
    }

    /// <summary>
    /// OneBot WS主机配置。
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class CqWsHostConfig
    {
        /// <summary>
        /// 是否启用WS。
        /// </summary>
        [JsonProperty("enable")]
        public bool Enable { get; set; }

        /// <summary>
        /// WS服务器监听的IP。
        /// </summary>
        [JsonProperty("host")]
        public string Host { get; set; } = "0.0.0.0";

        /// <summary>
        /// WS服务器监听的端口。
        /// </summary>
        [JsonProperty("port")]
        public int Port { get; set; } = 6700;
    }

    /// <summary>
    /// OneBot WS Reverse主机配置。
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class CqWsReverseHostConfig
    {
        /// <summary>
        /// 是否启用WS Reverse。
        /// </summary>
        [JsonProperty("enable")]
        public bool Enable { get; set; }
    }

    /// <summary>
    /// OneBot鉴权配置。
    /// </summary>
    public class CqAuthConfig
    {
        /// <summary>
        /// OneBot 鉴权Access Token。
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = "";
    }
}
