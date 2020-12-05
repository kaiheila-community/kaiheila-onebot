using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaiheila.Events;
using Kaiheila.OneBot.Storage;
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
            switch (Context.ConfigHelper.Config.KhConfig.KhSendingMode)
            {
                case KhSendingMode.Normal:
                    long channel = long.Parse(payload["group_id"]?.ToObject<string>()!);
                    Context.KhHost.Bot.SendEvents(
                        new List<KhEventBase>(
                            Context.CqMessageHost.Parse(payload["message"]).CodeList
                                .Select(x =>
                                {
                                    Task<KhEventBase> convertTask = x.ConvertToKhEvent(
                                        Context,
                                        channel);
                                    convertTask.Wait();
                                    return convertTask.Result;
                                })),
                        new KhEventBase
                        {
                            ChannelId = channel
                        },
                        Context.KhEventCombinerHost).Wait();
                    break;
                case KhSendingMode.Plain:
                    Context.KhHost.Bot.SendTextMessage(long.Parse(payload["group_id"]?.ToObject<string>()!),
                        Context.CqMessageHost.Parse(payload["message"]).CodeList
                            .Aggregate("", (s, b) =>
                            {
                                Task<string> taskString = b.ConvertToString();
                                taskString.Wait();
                                return s + taskString.Result;
                            })).Wait();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

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
