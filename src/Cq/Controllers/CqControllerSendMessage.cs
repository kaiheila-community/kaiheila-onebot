using System.Linq;
using Newtonsoft.Json.Linq;

namespace Kaiheila.OneBot.Cq.Controllers
{
    [CqController("send_private_msg")]
    public class CqControllerSendPrivateMessage : CqControllerBase
    {
        public CqControllerSendPrivateMessage(CqContext context) : base(context)
        {
        }

        public override JToken Process(JToken payload)
        {
            throw new System.NotImplementedException();
        }
    }

    [CqController("send_group_msg")]
    public class CqControllerSendGroupMessage : CqControllerBase
    {
        public CqControllerSendGroupMessage(CqContext context) : base(context)
        {
        }

        public override JToken Process(JToken payload)
        {
            Context.KhHost.Bot.SendTextMessage(long.Parse(payload["group_id"]?.ToObject<string>()!),
                Context.CqMessageHost.Parse(payload["message"]).CodeList
                    .Aggregate("", (s, b) => s + b.ConvertToString()));

            return JToken.FromObject(new
            {
                message_id = 0
            });
        }
    }

    [CqController("send_msg")]
    public class CqControllerSendMessage : CqControllerBase
    {
        public CqControllerSendMessage(CqContext context) : base(context)
        {
        }

        public override JToken Process(JToken payload)
        {
            throw new System.NotImplementedException();
        }
    }
}
