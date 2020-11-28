namespace Kaiheila.Cqhttp.Cq.Code
{
    [CqCodeType("text")]
    public class CqCodeText : CqCode
    {
        public CqCodeText(CqCode cqCode)
        {
            Params = cqCode.Params;
            Text = Params["text"];
        }

        public CqCodeText(string text)
        {
            Text = text;
        }

        public readonly string Text;
    }
}
