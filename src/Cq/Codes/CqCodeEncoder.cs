// References:
// https://github.com/frank-bots/cqhttp.Cyan/blob/master/cqhttp.Cyan/Globals.cs

namespace Kaiheila.Cqhttp.Cq.Codes
{
    public static class CqCodeEncoder
    {
        public static string EncodeText(string enc) =>
            enc
                .Replace("&", "&amp;")
                .Replace("[", "&#91;")
                .Replace("]", "&#93;");

        public static string EncodeValue(string text) => 
            EncodeText(text)
                .Replace(",", "&#44;");

        public static string Decode(string enc) =>
            enc
                .Replace("&#91;", "[")
                .Replace("&#93;", "]")
                .Replace("&#44;", ",")
                .Replace("&amp;", "&");
    }
}
