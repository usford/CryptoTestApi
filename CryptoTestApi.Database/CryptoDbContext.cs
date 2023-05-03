using CryptoTestApi.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoTestApi.Database
{
    public class CryptoDbContext : DbContext
    {
        public DbSet<Wallet> Wallets { get; set; }

        public CryptoDbContext(DbContextOptions<CryptoDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
