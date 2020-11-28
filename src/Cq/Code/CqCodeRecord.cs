namespace Kaiheila.Cqhttp.Cq.Code
{
    [CqCode("record")]
    public class CqCodeRecord : CqCodeBase
    {
        public CqCodeRecord(CqCodeRaw cqCode)
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
