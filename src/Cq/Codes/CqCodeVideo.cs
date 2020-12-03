namespace Kaiheila.OneBot.Cq.Codes
{
    [CqCode("video")]
    public class CqCodeVideo : CqCodeBase
    {
        public CqCodeVideo(CqCodeRaw cqCode)
        {
            Params = cqCode.Params;
            File = Params["file"];
            Url = Params.ContainsKey("url") ? Params["url"] : string.Empty;
        }

        public CqCodeVideo(string file, string url)
        {
            File = file;
            Url = url;
        }

        public readonly string File;

        public readonly string Url;

        public override string ConvertToString() => $"（视频：{Url} ）";
    }
}
