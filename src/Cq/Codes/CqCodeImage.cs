using System.Threading.Tasks;
using Kaiheila.Events;
using Kaiheila.OneBot.Cq.Database;
using Microsoft.EntityFrameworkCore;

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

        public override async Task<KhEventBase> ConvertToKhEvent(CqContext context, long channel)
        {
            CqDatabaseContext database = new CqDatabaseContext();

            try
            {
                CqAsset asset = await database.Assets.SingleOrDefaultAsync(x => x.Url == Url);

                if (asset is null)
                {
                    KhEventImage image = await context.KhHost.Bot.UploadImage(
                        File,
                        channel,
                        Url);

                    await database.Assets.AddAsync(new CqAsset
                    {
                        Path = image.Path,
                        Url = Url,
                        Name = File
                    });

                    await database.SaveChangesAsync();

                    return image;
                }
                else
                {
                    return new KhEventImage(
                        asset.Path,
                        asset.Name);
                }
            }
            finally
            {
                await database.DisposeAsync();
            }
        }

        public override async Task<string> ConvertToString() => $"（图片：{Url} ）";
    }
}
