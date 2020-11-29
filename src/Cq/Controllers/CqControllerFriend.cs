using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaiheila.Data;
using Newtonsoft.Json.Linq;

namespace Kaiheila.Cqhttp.Cq.Controllers
{
    [CqController("get_login_info")]
    public class CqControllerGetLoginInfo : CqControllerBase
    {
        public CqControllerGetLoginInfo(CqContext context) : base(context)
        {
            _context = context;
        }

        private readonly CqContext _context;

        public override JToken Process(JToken payload)
        {
            Task<KhUser> getUserStateAsync = _context.KhHost.Bot.GetUserState();
            getUserStateAsync.Wait();
            KhUser user = getUserStateAsync.Result;
            return JObject.FromObject(new
            {
                user_id = user.Id,
                nickname = user.Username
            });
        }
    }

    [CqController("get_stranger_info")]
    public class CqControllerGetStrangerInfo : CqControllerBase
    {
        public CqControllerGetStrangerInfo(CqContext context) : base(context)
        {
        }

        public override JToken Process(JToken payload)
        {
            throw new System.NotImplementedException();
        }
    }

    [CqController("get_friend_list")]
    public class CqControllerGetFriendList : CqControllerBase
    {
        public CqControllerGetFriendList(CqContext context) : base(context)
        {
            _context = context;
        }

        private readonly CqContext _context;

        public override JToken Process(JToken payload)
        {
            Task<List<KhUser>> getFriendsAsync = _context.KhHost.Bot.GetFriends();
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
