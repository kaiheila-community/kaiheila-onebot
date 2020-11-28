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
    }
}
