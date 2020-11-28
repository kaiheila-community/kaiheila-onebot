namespace Kaiheila.Cqhttp.Cq.Code
{
    [CqCodeType("video")]
    public class CqCodeVideo : CqCode
    {
        public CqCodeVideo(CqCode cqCode)
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
