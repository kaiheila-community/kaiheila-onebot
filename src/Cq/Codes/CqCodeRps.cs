using System.Threading.Tasks;

namespace Kaiheila.OneBot.Cq.Codes
{
    [CqCode("rps")]
    public class CqCodeRps : CqCodeBase
    {
        public CqCodeRps(CqCodeRaw cqCode)
        {
            Params = cqCode.Params;
        }

        public override async Task<string> ConvertToString() => "（猜拳）";
    }
}
