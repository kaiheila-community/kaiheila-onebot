namespace Kaiheila.OneBot.Cq.Codes
{
    [CqCode("text")]
    public class CqCodeText : CqCodeBase
    {
        public CqCodeText(CqCodeRaw cqCode)
        {
            Params = cqCode.Params;
            Text = Params["text"];
        }

        public CqCodeText(string text)
        {
            Text = text;
        }

        public readonly string Text;

        public override string ConvertToString() => Text;
    }
}
