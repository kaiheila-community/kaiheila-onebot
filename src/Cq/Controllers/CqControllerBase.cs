using System;
using Kaiheila.Cqhttp.Storage;
using Newtonsoft.Json.Linq;

namespace Kaiheila.Cqhttp.Cq.Controllers
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
        public CqControllerBase(CqControllerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 执行任务。
        /// </summary>
        /// <param name="payload">JSON报文。</param>
        public abstract void Process(JObject payload);

        /// <summary>
        /// 任务上下文。
        /// </summary>
        private readonly CqControllerContext _context;
    }

    /// <summary>
    /// CQHTTP任务控制器上下文。
    /// </summary>
    public sealed class CqControllerContext
    {
        /// <summary>
        /// 提供访问应用配置能力的帮助类型。
        /// </summary>
        public ConfigHelper ConfigHelper;
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
