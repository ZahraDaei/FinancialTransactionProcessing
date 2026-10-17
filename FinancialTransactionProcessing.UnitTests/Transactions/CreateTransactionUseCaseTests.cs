using FinancialTransactionProcessing.Application.Abstractions.Repositories;
using FinancialTransactionProcessing.Application.Transactions;
using FinancialTransactionProcessing.Domain.Entities;
using Moq;

namespace FinancialTransactionProcessing.UnitTests.Transactions
{
    public class CreateTransactionUseCaseTests
    {
        [Fact]
        public async Task Execute_WithValidInput_ShouldCreatePendingTransaction()
        {
            var moqTransactionRepository = new Mock<ITransactionRepository>();

            var transactionExecuter = new CreateTransactionUseCase(moqTransactionRepository.Object);

            Transaction save = null;
            moqTransactionRepository.Setup(z => z.AddAsync(It.IsAny<Transaction>(), It.IsAny<CancellationToken>()))
                                    .Callback<Transaction, CancellationToken>((x, y) => save = x);
            var transaction = await transactionExecuter.Execute("1234567891234",
                                                          "4567891236541",
                                                          156000,
                                                          "IRR",
                                                          CancellationToken.None);
            moqTransactionRepository.Verify(x => x.AddAsync(It.Is<Transaction>(x => x.Amount == 156000 && x.Status == TransactionStatus.Pending),
                                                            It.IsAny<CancellationToken>()), Times.Once);



            Assert.Equal(TransactionStatus.Pending, save.Status);
            Assert.Equal(156000, save.Amount);
            Assert.Equal("1234567891234", save.SourceAccount.Value);
            Assert.Equal("4567891236541", save.DestinationAccount.Value);
            Assert.Equal("IRR", save.Currency.Value);
        }
        [Fact]
        public async Task Execute_WithWrongInput_ShouldThrow()
        {
            var moqTransactionRepository = new Mock<ITransactionRepository>();

            var transactionExecuter = new CreateTransactionUseCase(moqTransactionRepository.Object);

            Func<Task> transactionCreation = () => transactionExecuter.Execute("123456734",
                                                          "4567891236541",
                                                          156000,
                                                          "IRR",
                                                          CancellationToken.None);
            await Assert.ThrowsAsync<ArgumentException>(transactionCreation);

            moqTransactionRepository.Verify(x => x.AddAsync(It.IsAny<Transaction>(), It.IsAny<CancellationToken>()), Times.Never);

        }
    }
}
