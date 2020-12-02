using Kaiheila.Events;
using Newtonsoft.Json.Linq;

namespace Kaiheila.Cqhttp.Cq.Events
{
    [CqEvent(typeof(KhEventTextMessage))]
    public class CqEventGroupMessage : CqEventBase
    {
        public CqEventGroupMessage(CqContext context, KhEventBase eventBase) : base(context)
        {
            if (!(eventBase is KhEventTextMessage eventMessage)) return;

            Result = _context.CreateEventObject(CqEventPostType.Message);
            Result["message_type"] = "group";
            Result["sub_type"] = "normal";
            Result["message_id"] = 0;
            Result["group_id"] = eventMessage.ChannelId;
            Result["user_id"] = eventMessage.User.Id;
            Result["anonymous"] = null;
            Result["message"] = eventMessage.Content;
            Result["raw_message"] = eventMessage.Content;
            Result["font"] = 0;
            Result["sender"] = JToken.FromObject(new
            {
                user_id = eventMessage.User.Id,
                nickname = eventMessage.User.Username,
                card = "",
                sex = "unknown",
                age = 18,
                area = "",
                level = "",
                role = "member",
                title = ""
            });
        }
    }
}
