using System;
using System.Linq;
using Kaiheila.OneBot.Cq.Codes;
using Xunit;

namespace Kaiheila.OneBot.Test.Cq.Code
{
    public class CqCodes
    {
        [Fact]
        public static void CqCodeTypesBasedOnCqCode()
        {
            foreach (Type type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(x =>
                x.GetTypes()
                    .Where(type => Attribute.GetCustomAttribute(type, typeof(CqCodeAttribute)) is not null)))
                Assert.Equal(typeof(CqCodeBase), type.BaseType);
        }
    }
}
