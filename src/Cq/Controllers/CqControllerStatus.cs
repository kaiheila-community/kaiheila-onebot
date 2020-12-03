using System;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Kaiheila.OneBot.Cq.Controllers
{
    [CqController("get_status")]
    public class CqControllerGetStatus : CqControllerBase
    {
        public CqControllerGetStatus(CqContext context) : base(context)
        {
        }

        public override JToken Process(JToken payload) =>
            JToken.FromObject(new
            {
                online = true,
                good = true
            });
    }

    [CqController("get_version_info")]
    public class CqControllerGetVersionInfo : CqControllerBase
    {
        public CqControllerGetVersionInfo(CqContext context) : base(context)
        {
        }

        public override JToken Process(JToken payload) => 
            JToken.FromObject(new
            {
                app_name = "kaiheila-cqhttp",
                app_version = Assembly.GetExecutingAssembly().GetName().Version?.ToString(),
                protocol_version = "v11",
                coolq_edition = "pro",
                coolq_directory = AppContext.BaseDirectory,
                plugin_version = Assembly.GetExecutingAssembly().GetName().Version?.ToString()
            });
    }

    [CqController("set_restart")]
    public class CqControllerSetRestart : CqControllerBase
    {
        public CqControllerSetRestart(CqContext context) : base(context)
        {
        }

        public override JToken Process(JToken payload) => JToken.FromObject(new{});
    }

    [CqController("clean_cache")]
    public class CqControllerCleanCache : CqControllerBase
    {
        public CqControllerCleanCache(CqContext context) : base(context)
        {
        }

        public override JToken Process(JToken payload) => JToken.FromObject(new { });
    }
}
