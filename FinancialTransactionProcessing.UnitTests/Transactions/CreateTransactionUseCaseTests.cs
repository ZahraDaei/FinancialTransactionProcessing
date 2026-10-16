using FinancialTransactionProcessing.Application.Abstractions.Repositories;
using FinancialTransactionProcessing.Application.Transactions;
using FinancialTransactionProcessing.Domain.Entities;
using Moq;

namespace FinancialTransactionProcessing.UnitTests.Transactions
{
    public class CreateTransactionUseCaseTests
    {
        [Fact]
        public async void Execute_WithValidInput_ShouldCreatePendingTransaction()
        {
            var moqTransactionRepository = new Mock<ITransactionRepository>();
            var newTransaction = new Transaction(
                  "1234567891234",
                  "4567891236541",
                  156000,
                  "IRR");
            moqTransactionRepository.Setup(x => x.AddAsync(newTransaction, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            var transactionExecuter = new CreateTransactionUseCase(moqTransactionRepository.Object);

            var transaction = await transactionExecuter.Execute("1234567891234",
                                                          "4567891236541",
                                                          156000,
                                                          "IRR",
                                                          It.IsAny<CancellationToken>());

            Assert.Equal(TransactionStatus.Pending, transaction.Status);
            Assert.Equal(newTransaction.Amount, transaction.Amount);
            Assert.Equal(newTransaction.SourceAccount, transaction.SourceAccount);
            Assert.Equal(newTransaction.DestinationAccount, transaction.DestinationAccount);
            Assert.Equal(newTransaction.Currency, transaction.Currency);
        }
    }
}
