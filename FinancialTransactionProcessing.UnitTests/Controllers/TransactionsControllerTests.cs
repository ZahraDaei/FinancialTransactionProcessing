using FinancialTransactionProcessing.Application.Transactions;
using FinancialTransactionProcessing.Domain.Entities;
using Moq;

namespace FinancialTransactionProcessing.UnitTests.Controllers;

public class TransactionsControllerTests
{
    [Fact]
    public async Task CreateTransaction_WithValidData_ReturnsCreated()
    {
        var transaction = new Transaction(
            "1234567891234",
            "4567891236541",
            156000,
            "IRR");

        var useCaseMock = new Mock<CreateTransactionUseCase>(
            Mock.Of<FinancialTransactionProcessing.Application.Abstractions.Repositories.ITransactionRepository>());

        useCaseMock.Setup(x => x.Execute(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(transaction);

        // این تست نیازمند قابل Mock بودن UseCase است.
    }
}
