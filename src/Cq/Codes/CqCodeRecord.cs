﻿using System.Threading.Tasks;

namespace Kaiheila.OneBot.Cq.Codes
{
    [CqCode("record")]
    public class CqCodeRecord : CqCodeBase
    {
        public CqCodeRecord(CqCodeRaw cqCode)
        {
            Params = cqCode.Params;
            File = Params["file"];
            Url = Params.ContainsKey("url") ? Params["url"] : string.Empty;
        }

        public CqCodeRecord(string file, string url)
        {
            File = file;
            Url = url;
        }

        public readonly string File;

        public readonly string Url;

        public override async Task<string> ConvertToString() => $"（语音：{Url} ）";
    }
}
