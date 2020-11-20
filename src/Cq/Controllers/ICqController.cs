using System;
using Newtonsoft.Json.Linq;

namespace Kaiheila.Cqhttp.Cq.Controllers
{
    /// <summary>
    /// CQHTTP任务控制器。
    /// </summary>
    public interface ICqController
    {
        /// <summary>
        /// 执行任务。
        /// </summary>
        /// <param name="token">JSON报文。</param>
        public void Process(JToken token);
    }

    /// <summary>
    /// CQHTTP任务控制器。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class CqControllerAttribute : Attribute
    {
        /// <summary>
        /// 初始化CQHTTP任务控制器。
        /// </summary>
        /// <param name="action">控制器的任务。</param>
        public CqControllerAttribute(string action) => Action = action;

        /// <summary>
        /// 控制器的任务。
        /// </summary>
        public readonly string Action;
    }
}
