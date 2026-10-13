using FinancialTransactionProcessing.Domain.Entities;
using FinancialTransactionProcessing.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialTransactionProcessing.Infrastructure.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {

            builder.HasKey(t => t.Id);

            builder.Property(t => t.SourceAccount)
                .HasConversion(
                    account => account.Value,
                    value => new AccountNumber(value))
                .HasMaxLength(13)
                .IsRequired();

            builder.Property(t => t.DestinationAccount)
                .HasConversion(
                    account => account.Value,
                    value => new AccountNumber(value))
                .HasMaxLength(13)
                .IsRequired();

            builder.Property(t => t.Currency)
                .HasConversion(
                    currency => currency.Value,
                    value => new Currency(value))
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(t => t.Amount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(t => t.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(t => t.CreatedAt)
                .IsRequired();

        }
    }
}
