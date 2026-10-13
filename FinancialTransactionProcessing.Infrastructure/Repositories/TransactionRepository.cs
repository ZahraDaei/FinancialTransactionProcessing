using FinancialTransactionProcessing.Application.Abstractions.Repositories;
using FinancialTransactionProcessing.Domain.Entities;
using FinancialTransactionProcessing.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialTransactionProcessing.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionDbContext _context;

        public TransactionRepository(TransactionDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default)
        {
            await _context.Transactions.AddAsync(transaction, cancellationToken);
            await _context.SaveChangesAsync();
        }

        public async Task<Transaction?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Transactions
                .FirstOrDefaultAsync(
                    t => t.Id == id,
                    cancellationToken);
        }
    }
}
