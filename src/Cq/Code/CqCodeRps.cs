namespace Kaiheila.Cqhttp.Cq.Code
{
    [CqCode("rps")]
    public class CqCodeRps : CqCodeBase
    {
        public CqCodeRps(CqCodeRaw cqCode)
        {
            Params = cqCode.Params;
        }
    }
}
