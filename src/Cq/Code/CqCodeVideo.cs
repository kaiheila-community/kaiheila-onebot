namespace Kaiheila.Cqhttp.Cq.Code
{
    [CqCode("video")]
    public class CqCodeVideo : CqCodeBase
    {
        public CqCodeVideo(CqCodeRaw cqCode)
        {
            Params = cqCode.Params;
            File = Params["file"];
        }

        public CqCodeVideo(string file)
        {
            File = file;
        }

        public readonly string File;
    }
}
