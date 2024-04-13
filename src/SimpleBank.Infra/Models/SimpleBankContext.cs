using Microsoft.EntityFrameworkCore;
using SimpleBank.Core.Domains.Entities;

namespace SimpleBank.Infra.Models
{
    public class SimpleBankContext : DbContext
    {
        public SimpleBankContext(DbContextOptions<SimpleBankContext> options)
            : base (options) 
        {
        }
        public DbSet<Account> Accounts { get; set; } 
        public DbSet<Card> Cards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
