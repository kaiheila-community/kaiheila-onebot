namespace Kaiheila.Cqhttp.Cq.Code
{
    [CqCodeType("dice")]
    public class CqCodeDice : CqCode
    {
        public CqCodeDice(CqCode cqCode)
        {
            Params = cqCode.Params;
        }
    }
}
