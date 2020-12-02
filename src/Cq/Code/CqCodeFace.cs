namespace Kaiheila.Cqhttp.Cq.Code
{
    [CqCode("face")]
    public class CqCodeFace : CqCodeBase
    {
        public CqCodeFace(CqCodeRaw cqCode)
        {
            Params = cqCode.Params;
            Id = ushort.Parse(Params["id"]);
        }

        public CqCodeFace(ushort id)
        {
            Id = id;
        }

        public readonly ushort Id;

        public override string ConvertToString() => $"（表情：{Id} ）";
    }
}
