using System.Collections.Generic;

namespace Kaiheila.OneBot.Cq.Codes
{
    [CqCode("raw")]
    public class CqCodeRaw : CqCodeBase
    {
        public CqCodeRaw()
        {

        }

        public CqCodeRaw(Dictionary<string, string> @params)
        {
            Params = @params;
        }

        public override string ConvertToString() => "（不支持的消息）";
    }
}
