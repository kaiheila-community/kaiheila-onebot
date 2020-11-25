namespace Kaiheila.Cqhttp.Cq.Code
{
    public static class CqCodeEncoder
    {
        public static string Decode(string enc) =>
            enc
                .Replace("&#91;", "[")
                .Replace("&#93;", "]")
                .Replace("&#44;", ",")
                .Replace("&amp;", "&");
    }
}
