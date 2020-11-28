using System;
using System.Collections.Generic;

namespace Kaiheila.Cqhttp.Cq.Code
{
    public abstract class CqCodeBase
    {
        public Dictionary<string, string> Params = new Dictionary<string, string>();
    }

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class CqCodeAttribute : Attribute
    {
        public CqCodeAttribute(string type) => Type = type;

        public readonly string Type;
    }
}
