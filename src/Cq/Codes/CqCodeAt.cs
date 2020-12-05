using System.Threading.Tasks;

namespace Kaiheila.OneBot.Cq.Codes
{
    [CqCode("at")]
    public class CqCodeAt : CqCodeBase
    {
        public CqCodeAt(CqCodeRaw cqCode)
        {
            Params = cqCode.Params;
            Target = Params["qq"];
        }

        public CqCodeAt(string target)
        {
            Target = target;
        }

        public readonly string Target;

        public override async Task<string> ConvertToString() => $"（@：{Target} ）";
    }
}
