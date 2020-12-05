using Kaiheila.OneBot.Storage;
using Microsoft.EntityFrameworkCore;

namespace Kaiheila.OneBot.Cq.Database
{
    public sealed class CqDatabaseContext : DbContext
    {
        public CqDatabaseContext()
        {
        }

        public CqDatabaseContext(DbContextOptions<CqDatabaseContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(@$"Data Source={StorageHelper.GetRootFilePath("database.db")}");
        }

        public DbSet<CqAsset> Assets { get; set; }
    }
}
