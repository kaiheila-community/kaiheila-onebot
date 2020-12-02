using System;
using System.Linq;
using Kaiheila.Cqhttp.Cq.Events;
using Xunit;

namespace Kaiheila.Cqhttp.Test.Cq.Events
{
    public static class CqEvents
    {
        [Fact]
        public static void CqEventBasedOnCqEventBase()
        {
            foreach (Type type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(x =>
                x.GetTypes()
                    .Where(type => Attribute.GetCustomAttribute(type, typeof(CqEventAttribute)) is not null)))
                Assert.Equal(typeof(CqEventBase), type.BaseType);
        }
    }
}
