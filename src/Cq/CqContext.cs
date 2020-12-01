using System.Composition;
using Kaiheila.Cqhttp.Cq.Message;
using Kaiheila.Cqhttp.Kh;
using Kaiheila.Cqhttp.Storage;

namespace Kaiheila.Cqhttp.Cq
{
    /// <summary>
    /// CQHTTP上下文。
    /// </summary>
    [Export]
    public class CqContext
    {
        /// <summary>
        /// 初始化CQHTTP上下文。
        /// </summary>
        /// <param name="khHost">Kaiheila主机。</param>
        /// <param name="configHelper">提供访问应用配置能力的帮助类型。</param>
        /// <param name="cqMessageHost">CQ消息主机。</param>
        public CqContext(
            KhHost khHost,
            ConfigHelper configHelper,
            CqMessageHost cqMessageHost)
        {
            KhHost = khHost;
            ConfigHelper = configHelper;
            CqMessageHost = cqMessageHost;
        }

        /// <summary>
        /// Kaiheila主机。
        /// </summary>
        public readonly KhHost KhHost;

        /// <summary>
        /// 提供访问应用配置能力的帮助类型。
        /// </summary>
        public readonly ConfigHelper ConfigHelper;

        /// <summary>
        /// CQ消息主机。
        /// </summary>
        public readonly CqMessageHost CqMessageHost;
    }
}
