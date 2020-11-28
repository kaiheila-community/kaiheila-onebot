namespace Kaiheila.Cqhttp.Cq.Code
{
    [CqCodeType("record")]
    public class CqCodeRecord : CqCode
    {
        public CqCodeRecord(CqCode cqCode)
        {
            Params = cqCode.Params;
            File = Params["file"];
        }

        public CqCodeRecord(string file)
        {
            File = file;
        }

        public readonly string File;
    }
}
