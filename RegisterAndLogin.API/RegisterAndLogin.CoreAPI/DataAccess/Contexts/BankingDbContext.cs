using Microsoft.EntityFrameworkCore;
using RegisterAndLogin.CoreAPI.Entities;
using System.Collections.Generic;

namespace RegisterAndLogin.CoreAPI.DataAccess.Contexts
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options)
        {
        }

        public DbSet<Musteriler> Musteriler { get; set; }
        public DbSet<MusteriBankaHesaplari> MusteriBankaHesaplari { get; set; }
    }
}
