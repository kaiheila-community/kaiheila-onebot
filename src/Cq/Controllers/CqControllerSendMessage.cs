using Newtonsoft.Json.Linq;

namespace Kaiheila.Cqhttp.Cq.Controllers
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
            //_context.KhHost.Bot.SendMessage(
            //    long.Parse(payload["group_id"]?.ToObject<string>()!),
            //    payload["auto_escape"].ToObject<bool>()
            //        ? payload["message"]?.ToObject<string>()
            //        : _context.CqMessageHost.Parse(payload["message"]?.ToObject<string>()).ToString());

            Context.KhHost.Bot.SendMessage(
                long.Parse(payload["group_id"]?.ToObject<string>()!),
                payload["message"]?.ToObject<string>());

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
