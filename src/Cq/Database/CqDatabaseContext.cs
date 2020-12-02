using Microsoft.EntityFrameworkCore;

namespace Kaiheila.Cqhttp.Cq.Database
{
    public sealed class CqDatabaseContext : DbContext
    {
        public CqDatabaseContext(DbContextOptions<CqDatabaseContext> options) : base(options)
        {
        }
    }
}
