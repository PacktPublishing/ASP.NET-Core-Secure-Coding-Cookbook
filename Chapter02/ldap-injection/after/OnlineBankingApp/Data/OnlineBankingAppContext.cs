using Microsoft.EntityFrameworkCore;

namespace OnlineBankingApp.Data
{
    public class OnlineBankingAppContext : DbContext
    {
        public OnlineBankingAppContext (
            DbContextOptions<OnlineBankingAppContext> options)
            : base(options)
        {
        }

        public DbSet<OnlineBankingApp.Models.Customer> Customer { get; set; }
        public DbSet<OnlineBankingApp.Models.Account> Account { get; set; }
        public DbSet<OnlineBankingApp.Models.FundTransfer> FundTransfer { get; set; }
        public DbSet<OnlineBankingApp.Models.Backup> Backup { get; set; }
    
    }
}