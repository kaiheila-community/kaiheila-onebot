namespace Kaiheila.OneBot.Cq.Codes
{
    [CqCode("dice")]
    public class CqCodeDice : CqCodeBase
    {
        public CqCodeDice(CqCodeRaw cqCode)
        {
            Params = cqCode.Params;
        }

        public override string ConvertToString() => "（掷骰子）";
    }
}
