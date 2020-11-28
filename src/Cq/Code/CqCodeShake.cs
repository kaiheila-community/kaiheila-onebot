namespace Kaiheila.Cqhttp.Cq.Code
{
    [CqCode("shake")]
    public class CqCodeShake : CqCodeBase
    {
        public CqCodeShake(CqCodeRaw cqCode)
        {
            Params = cqCode.Params;
        }
    }
}
