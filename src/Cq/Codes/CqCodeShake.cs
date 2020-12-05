using System.Threading.Tasks;

namespace Kaiheila.OneBot.Cq.Codes
{
    [CqCode("shake")]
    public class CqCodeShake : CqCodeBase
    {
        public CqCodeShake(CqCodeRaw cqCode)
        {
            Params = cqCode.Params;
        }

        public override async Task<string> ConvertToString() => "（窗口抖动）";
    }
}
