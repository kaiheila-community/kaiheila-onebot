using System.Composition;

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
        public readonly Bot Bot;
    }
}
