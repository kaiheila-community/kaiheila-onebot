using System;
using Newtonsoft.Json.Linq;

namespace Kaiheila.Cqhttp.Cq.Events
{
    public class CqEventBase
    {
        public JObject Result;
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    sealed class CqEventAttribute : Attribute
    {
        public CqEventAttribute(Type type) => Type = type;

        public readonly Type Type;
    }
}
