using System.Collections.Generic;

namespace Kaiheila.Cqhttp.Cq.Code
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
