using Newtonsoft.Json;

namespace Kaiheila.Cqhttp.Storage
{
    /// <summary>
    /// 应用配置。
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Config
    {
        /// <summary>
        /// CQHTTP配置。
        /// </summary>
        [JsonProperty("cqhttp")]
        public CqConfig CqConfig { get; set; } = new CqConfig();
    }

    /// <summary>
    /// CQHTTP配置。
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class CqConfig
    {
        /// <summary>
        /// CQHTTP HTTP主机配置。
        /// </summary>
        [JsonProperty("http")]
        public CqHttpHostConfig CqHttpHostConfig { get; set; } = new CqHttpHostConfig();
    }

    /// <summary>
    /// CQHTTP HTTP主机配置。
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
}
