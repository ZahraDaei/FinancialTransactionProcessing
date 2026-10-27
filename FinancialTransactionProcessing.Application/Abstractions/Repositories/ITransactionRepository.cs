using FinancialTransactionProcessing.Domain.Entities;

namespace FinancialTransactionProcessing.Application.Abstractions.Repositories
{
    public interface ITransactionRepository
    {
        Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default);
        Task<Transaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);


        Task<List<Transaction>> GetAllAsync(
            int page,
            int pageSize,
            CancellationToken cancellationToken = default);

        Task<int> GetCountAsync(
            CancellationToken cancellationToken = default);

    }
}
