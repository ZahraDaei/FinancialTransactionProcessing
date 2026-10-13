using FinancialTransactionProcessing.Application.Abstractions.Repositories;
using FinancialTransactionProcessing.Domain.Entities;


namespace FinancialTransactionProcessing.Application.Transactions
{
    public class CreateTransactionUseCase
    {
        private readonly ITransactionRepository _transactionRepository;

        public CreateTransactionUseCase(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<Transaction> Execute(string sourceAccount, string destinationAccount, decimal amount, string currency, CancellationToken cancellationToken)
        {
            Transaction transaction = new Transaction(sourceAccount, destinationAccount, amount, currency);
            await _transactionRepository.AddAsync(transaction, cancellationToken);
            return transaction;
        }
    }
}
