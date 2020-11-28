namespace Kaiheila.Cqhttp.Cq.Code
{
    [CqCodeType("shake")]
    public class CqCodeShake : CqCode
    {
        public CqCodeShake(CqCode cqCode)
        {
            Params = cqCode.Params;
        }
    }
}
