namespace Kaiheila.Cqhttp.Cq.Code
{
    [CqCodeType("at")]
    public class CqCodeAt : CqCode
    {
        public CqCodeAt(CqCode cqCode)
        {
            Params = cqCode.Params;
            Target = Params["qq"];
        }

        public CqCodeAt(string target)
        {
            Target = target;
        }

        public readonly string Target;
    }
}
