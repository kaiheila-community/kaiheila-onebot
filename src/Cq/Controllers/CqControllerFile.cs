using Newtonsoft.Json.Linq;

namespace Kaiheila.OneBot.Cq.Controllers
{
    [CqController("can_send_image")]
    public class CqControllerCanSendImage : CqControllerBase
    {
        public CqControllerCanSendImage(CqContext context) : base(context)
        {
        }

        public override JToken Process(JToken payload) =>
            JToken.FromObject(new
            {
                yes = true
            });
    }

    [CqController("can_send_record")]
    public class CqControllerCanSendRecord : CqControllerBase
    {
        public CqControllerCanSendRecord(CqContext context) : base(context)
        {
        }

        public override JToken Process(JToken payload) =>
            JToken.FromObject(new
            {
                yes = false
            });
    }
}
