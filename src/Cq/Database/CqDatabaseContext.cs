using Kaiheila.Cqhttp.Storage;
using Microsoft.EntityFrameworkCore;

namespace Kaiheila.Cqhttp.Cq.Database
{
    public sealed class CqDatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlite(@$"Data Source={StorageHelper.GetRootFilePath("database.db")}");
    }
}
