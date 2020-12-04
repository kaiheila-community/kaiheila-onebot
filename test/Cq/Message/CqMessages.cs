using Kaiheila.OneBot.Cq.Codes;
using Kaiheila.OneBot.Cq.Message;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Kaiheila.OneBot.Test.Cq.Message
{
    public class CqMessages
    {
        #region Data

        private const string StringToken = "[CQ:face,id=178]看看我刚拍的照片[CQ:image,file=123.jpg]";

        // ReSharper disable once InconsistentNaming
        private static readonly JArray ArrayToken = JArray.FromObject(
            new object[]
            {
                new
                {
                    type = "face",
                    data = new
                    {
                        id = "178"
                    }
                },
                new
                {
                    type = "text",
                    data = new
                    {
                        text = "看看我刚拍的照片"
                    }
                },
                new
                {
                    type = "image",
                    data = new
                    {
                        file = "123.jpg"
                    }
                }
            });

        #endregion

        private static void ParseMessage(JToken token)
        {
            CqMessageHost cqMessageHost = new CqMessageHost(new CqCodeHost(new Logger<CqCodeHost>(new NullLoggerFactory())));

            CqMessage message = cqMessageHost.Parse(token);

            Assert.Equal(3, message.CodeList.Count);

            Assert.IsType<CqCodeFace>(message.CodeList[0]);
            Assert.IsType<CqCodeText>(message.CodeList[1]);
            Assert.IsType<CqCodeImage>(message.CodeList[2]);

            Assert.Equal(178, ((CqCodeFace) message.CodeList[0]).Id);
            Assert.Equal("看看我刚拍的照片", ((CqCodeText) message.CodeList[1]).Text);
            Assert.Equal("123.jpg", ((CqCodeImage) message.CodeList[2]).File);
        }

        [Fact]
        public static void ParseStringMessage() => ParseMessage(JValue.CreateString(StringToken));

        [Fact]
        public static void ParseArrayMessage() => ParseMessage(ArrayToken);
    }
}
