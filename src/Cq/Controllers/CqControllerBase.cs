using System;
using Newtonsoft.Json.Linq;

namespace Kaiheila.OneBot.Cq.Controllers
{
    /// <summary>
    /// CQHTTP任务控制器。
    /// </summary>
    public abstract class CqControllerBase
    {
        /// <summary>
        /// 初始化CQHTTP任务控制器。
        /// </summary>
        /// <param name="context">任务上下文。</param>
        protected CqControllerBase(CqContext context)
        {
            Context = context;
        }

        /// <summary>
        /// 执行任务。
        /// </summary>
        /// <param name="payload">JSON报文。</param>
        public abstract JToken Process(JToken payload);

        /// <summary>
        /// 任务上下文。
        /// </summary>
        protected readonly CqContext Context;
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
