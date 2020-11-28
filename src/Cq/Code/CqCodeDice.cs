namespace Kaiheila.Cqhttp.Cq.Code
{
    [CqCode("dice")]
    public class CqCodeDice : CqCodeBase
    {
        public CqCodeDice(CqCodeRaw cqCode)
        {
            Params = cqCode.Params;
        }
    }
}
