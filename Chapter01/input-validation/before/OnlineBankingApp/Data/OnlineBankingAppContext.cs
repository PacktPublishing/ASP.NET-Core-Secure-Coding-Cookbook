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
    }
}