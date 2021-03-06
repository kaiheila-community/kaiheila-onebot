﻿using System;
using System.Linq;
using Kaiheila.OneBot.Cq.Controllers;
using Xunit;

namespace Kaiheila.OneBot.Test.Cq.Controllers
{
    public class CqControllers
    {
        [Fact]
        public static void CqControllerBasedOnCqControllerBase()
        {
            foreach (Type type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(x =>
                x.GetTypes()
                    .Where(type => Attribute.GetCustomAttribute(type, typeof(CqControllerAttribute)) is not null)))
                Assert.Equal(typeof(CqControllerBase), type.BaseType);
        }
    }
}
