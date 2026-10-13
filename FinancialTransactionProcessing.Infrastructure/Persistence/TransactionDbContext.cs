using FinancialTransactionProcessing.Domain.Entities;
using FinancialTransactionProcessing.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FinancialTransactionProcessing.Infrastructure.Persistence
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
        {

        }
        public DbSet<Transaction> Transactions => Set<Transaction>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        }
    }
}
