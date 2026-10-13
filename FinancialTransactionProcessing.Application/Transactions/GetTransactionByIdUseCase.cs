using FinancialTransactionProcessing.Application.Abstractions.Repositories;
using FinancialTransactionProcessing.Domain.Entities;

namespace FinancialTransactionProcessing.Application.Transactions
{
    public class GetTransactionByIdUseCase
    {
        private readonly ITransactionRepository _transactionRepository;

        public GetTransactionByIdUseCase(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<Transaction> Execute(Guid id, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id, cancellationToken);
            return transaction;
        }
    }
}
