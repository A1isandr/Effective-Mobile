using EffectiveMobile.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace EffectiveMobile.Backend.Common
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Db\\Orders.db");
        }
    }
}
