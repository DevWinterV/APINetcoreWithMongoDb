using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace APINetcoreWithSqLServer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Entities.Account> Accounts { get; set; }
    }
}
