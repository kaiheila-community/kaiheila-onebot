using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaiheila.Data;
using Newtonsoft.Json.Linq;

namespace Kaiheila.OneBot.Cq.Controllers
{
    [CqController("get_login_info")]
    public class CqControllerGetLoginInfo : CqControllerBase
    {
        public CqControllerGetLoginInfo(CqContext context) : base(context)
        {
        }

        public override JToken Process(JToken payload)
        {
            Task<KhUser> getUserStateAsync = Context.KhHost.Bot.GetUserState();
            getUserStateAsync.Wait();
            KhUser user = getUserStateAsync.Result;
            return JObject.FromObject(new
            {
                user_id = user.Id,
                nickname = user.Username
            });
        }
    }

    [CqController("get_friend_list")]
    public class CqControllerGetFriendList : CqControllerBase
    {
        public CqControllerGetFriendList(CqContext context) : base(context)
        {
        }

        public override JToken Process(JToken payload)
        {
            Task<List<KhUser>> getFriendsAsync = Context.KhHost.Bot.GetFriends(KhFriendsType.Friend);
            getFriendsAsync.Wait();
            List<KhUser> users = getFriendsAsync.Result;
            return JArray.FromObject(users.Select(user => new
            {
                user_id = user.Id,
                nickname = user.Username,
                remark = ""
            }));
        }
    }
}
