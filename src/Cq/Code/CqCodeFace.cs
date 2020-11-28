namespace Kaiheila.Cqhttp.Cq.Code
{
    [CqCodeType("face")]
    public class CqCodeFace : CqCode
    {
        public CqCodeFace(CqCode cqCode)
        {
            Params = cqCode.Params;
            Id = ushort.Parse(Params["id"]);
        }

        public CqCodeFace(ushort id)
        {
            Id = id;
        }

        public readonly ushort Id;
    }
}
