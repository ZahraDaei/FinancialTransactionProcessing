using FinancialTransactionProcessing.Application.Abstractions.Repositories;
using FinancialTransactionProcessing.Application.Transactions;
using FinancialTransactionProcessing.Domain.Entities;
using Moq;

namespace FinancialTransactionProcessing.UnitTests.Transactions
{
    public class GetTransactionByIdUseCaseTests
    {
        [Fact]
        public async void Execute_WhenTransactionExist_ReturnTransaction()
        {
            var id = new Guid("0d25c580-db58-44dd-bb0a-01a4a716237e");
            var expectedTransaction = new Transaction(
                    "1234567891234",
                    "4567891236541",
                    156000,
                    "IRR");

            var transactionRepositoryMoq = new Mock<ITransactionRepository>();

            transactionRepositoryMoq.Setup(x => x.GetByIdAsync(
                id,
                It.IsAny<CancellationToken>())).ReturnsAsync(() => expectedTransaction);

            var getUseCase = new GetTransactionByIdUseCase(transactionRepositoryMoq.Object);


            var transaction = await getUseCase.Execute(id, It.IsAny<CancellationToken>());

            Assert.NotNull(transaction);
            Assert.Equal(expectedTransaction.Amount, transaction.Amount);
            Assert.Equal(
                expectedTransaction.SourceAccount.Value,
                transaction.SourceAccount.Value);
        }
    }
}
