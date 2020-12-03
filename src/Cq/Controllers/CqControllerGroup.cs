using System.Threading.Tasks;
using Kaiheila.Data;
using Newtonsoft.Json.Linq;

namespace Kaiheila.OneBot.Cq.Controllers
{
    [CqController("get_stranger_info")]
    public class CqControllerGetStrangerInfo : CqControllerBase
    {
        public CqControllerGetStrangerInfo(CqContext context) : base(context)
        {
        }

        public override JToken Process(JToken payload)
        {
            Task<KhUser> getUserStateAsync = Context.KhHost.Bot.GetUserState(long.Parse(payload["user_id"]?.ToObject<string>()!));
            getUserStateAsync.Wait();
            KhUser user = getUserStateAsync.Result;

            return JToken.FromObject(new
            {
                user_id = user.Id,
                nickname = user.Username,
                sex = "unknown",
                age = 18
            });
        }
    }

    [CqController("get_group_member_info")]
    public class CqControllerGetGroupMemberInfo : CqControllerBase
    {
        public CqControllerGetGroupMemberInfo(CqContext context) : base(context)
        {
        }

        public override JToken Process(JToken payload)
        {
            Task<KhUser> getUserStateAsync = Context.KhHost.Bot.GetUserState(long.Parse(payload["user_id"]?.ToObject<string>()!));
            getUserStateAsync.Wait();
            KhUser user = getUserStateAsync.Result;

            return JToken.FromObject(new
            {
                user_id = user.Id,
                nickname = user.Username,
                card = "",
                sex = "unknown",
                age = 18,
                area = "",
                join_time = 0,
                last_sent_time = 0,
                level = "",
                role = "member",
                unfriendly = false,
                title = "",
                title_expire_time = 0,
                card_changeable = false
            });
        }
    }

    [CqController("get_group_info")]
    public class CqControllerGetGroupInfo : CqControllerBase
    {
        public CqControllerGetGroupInfo(CqContext context) : base(context)
        {
        }

        public override JToken Process(JToken payload)
        {
            Task<KhChannel> getChannelStateAsync = Context.KhHost.Bot.GetChannelState(long.Parse(payload["group_id"]?.ToObject<string>()!));
            getChannelStateAsync.Wait();
            KhChannel channel = getChannelStateAsync.Result;

            return JToken.FromObject(new
            {
                group_id = channel.ChannelId,
                group_name = channel.ChannelName,
                member_count = int.MaxValue,
                max_member_count = int.MaxValue
            });
        }
    }
}
