using System;
using System.Linq;
using Kaiheila.Cqhttp.Cq.Code;
using Xunit;

namespace Kaiheila.Cqhttp.Test.Cq.Code
{
    public class CqCodes
    {
        [Fact]
        public void CqCodeTypesBasedOnCqCode()
        {
            foreach (Type type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(x =>
                x.GetTypes()
                    .Where(type => Attribute.GetCustomAttribute(type, typeof(CqCodeTypeAttribute)) is not null)))
                Assert.Equal(typeof(CqCode), type.BaseType);
        }
    }
}
