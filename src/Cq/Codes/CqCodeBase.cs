using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kaiheila.Events;

namespace Kaiheila.OneBot.Cq.Codes
{
    public abstract class CqCodeBase
    {
        public Dictionary<string, string> Params = new Dictionary<string, string>();

        public virtual async Task<KhEventBase> ConvertToKhEvent(CqContext context) =>
            new KhEventTextMessage
            {
                Content = await ConvertToString()
            };

        public abstract Task<string> ConvertToString();
    }

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class CqCodeAttribute : Attribute
    {
        public CqCodeAttribute(string type) => Type = type;

        public readonly string Type;
    }
}
