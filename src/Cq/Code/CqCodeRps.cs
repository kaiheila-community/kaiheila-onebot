namespace Kaiheila.Cqhttp.Cq.Code
{
    [CqCodeType("rps")]
    public class CqCodeRps : CqCode
    {
        public CqCodeRps(CqCode cqCode)
        {
            Params = cqCode.Params;
        }
    }
}
