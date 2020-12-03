namespace Kaiheila.OneBot.Cq.Codes
{
    [CqCode("image")]
    public class CqCodeImage : CqCodeBase
    {
        public CqCodeImage(CqCodeRaw cqCode)
        {
            Params = cqCode.Params;
            File = Params["file"];
            Url = Params.ContainsKey("url") ? Params["url"] : string.Empty;
        }

        public CqCodeImage(string file, string url)
        {
            File = file;
            Url = url;
        }

        public readonly string File;

        public readonly string Url;

        public override string ConvertToString() => $"（图片：{Url} ）";
    }
}
