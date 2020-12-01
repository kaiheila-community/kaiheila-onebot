using System;
using System.Collections.Generic;

namespace Kaiheila.Cqhttp.Cq.Events
{
    public class CqEventBase
    {
        public Dictionary<string, string> Params = new Dictionary<string, string>();
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    sealed class CqEventAttribute : Attribute
    {
        public CqEventAttribute(Type type) => Type = type;

        public readonly Type Type;
    }
}
