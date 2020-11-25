using System;
using System.Collections.Generic;

namespace Kaiheila.Cqhttp.Cq.Code
{
    public class CqCode
    {
        public Dictionary<string, string> Params = new Dictionary<string, string>();
    }

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class CqCodeTypeAttribute : Attribute
    {
        public CqCodeTypeAttribute(string type) => Type = type;

        public readonly string Type;
    }
}
