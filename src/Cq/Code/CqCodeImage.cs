namespace Kaiheila.Cqhttp.Cq.Code
{
    [CqCodeType("image")]
    public class CqCodeImage : CqCode
    {
        public CqCodeImage(CqCode cqCode)
        {
            Params = cqCode.Params;
            File = Params["file"];
        }

        public CqCodeImage(string file)
        {
            File = file;
        }

        public readonly string File;
    }
}
