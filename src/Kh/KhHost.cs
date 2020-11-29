using System.Composition;
using Kaiheila.Client;

namespace Kaiheila.Cqhttp.Kh
{
    /// <summary>
    /// Kaiheila主机。
    /// </summary>
    [Export]
    public class KhHost
    {
        /// <summary>
        /// Kaiheila机器人。
        /// </summary>
        public readonly IBot Bot;
    }
}
