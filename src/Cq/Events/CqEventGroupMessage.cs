using Kaiheila.Events;
using Newtonsoft.Json.Linq;

namespace Kaiheila.Cqhttp.Cq.Events
{
    [CqEvent(typeof(KhEventMessage))]
    public class CqEventGroupMessage : CqEventBase
    {
        public CqEventGroupMessage(KhEventBase eventBase)
        {
            if (!(eventBase is KhEventMessage eventMessage)) return;

            Result = CqEventHelper.CreateEventObject(CqEventPostType.Message);
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
