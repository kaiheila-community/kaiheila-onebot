using System;
using System.Linq;
using Kaiheila.Cqhttp.Cq.Controllers;
using Xunit;

namespace Kaiheila.Cqhttp.Test.Cq.Controllers
{
    public class CqControllers
    {
        [Fact]
        public void CqControllerBasedOnCqControllerBase()
        {
            foreach (Type type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(x =>
                x.GetTypes()
                    .Where(type => Attribute.GetCustomAttribute(type, typeof(CqControllerAttribute)) is not null)))
                Assert.Equal(type.BaseType, typeof(CqControllerBase));
        }
    }
}
