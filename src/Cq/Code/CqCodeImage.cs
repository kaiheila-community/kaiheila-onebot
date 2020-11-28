namespace Kaiheila.Cqhttp.Cq.Code
{
    [CqCode("image")]
    public class CqCodeImage : CqCodeBase
    {
        public CqCodeImage(CqCodeRaw cqCode)
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
