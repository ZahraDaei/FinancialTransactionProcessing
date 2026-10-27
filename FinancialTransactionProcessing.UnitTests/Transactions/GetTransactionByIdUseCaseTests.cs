
using FinancialTransactionProcessing.Application.Abstractions.Repositories;
using FinancialTransactionProcessing.Application.Transactions;
using FinancialTransactionProcessing.Domain.Entities;
using Moq;

namespace FinancialTransactionProcessing.UnitTests.Transactions;

public class GetTransactionByIdUseCaseTests
{
    [Fact]
    public async Task Execute_WithEmptyId_ThrowsArgumentException()
    {
        var repositoryMock = new Mock<ITransactionRepository>();
        var useCase = new GetTransactionByIdUseCase(repositoryMock.Object);

        await Assert.ThrowsAsync<ArgumentException>(() =>
            useCase.Execute(Guid.Empty));

        repositoryMock.Verify(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Execute_WhenTransactionExists_ReturnsTransaction()
    {
        var id = Guid.NewGuid();

        var transaction = new Transaction(
            "1234567891234",
            "4567891236541",
            156000,
            "IRR");

        var repositoryMock = new Mock<ITransactionRepository>();

        repositoryMock.Setup(x => x.GetByIdAsync(
                id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(transaction);

        var useCase = new GetTransactionByIdUseCase(repositoryMock.Object);

        var result = await useCase.Execute(id);

        Assert.NotNull(result);

        repositoryMock.Verify(x => x.GetByIdAsync(
            id,
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Execute_WhenTransactionDoesNotExist_ReturnsNull()
    {
        var id = Guid.NewGuid();

        var repositoryMock = new Mock<ITransactionRepository>();

        repositoryMock.Setup(x => x.GetByIdAsync(
                id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Transaction?)null);

        var useCase = new GetTransactionByIdUseCase(repositoryMock.Object);

        var result = await useCase.Execute(id);

        Assert.Null(result);
    }


    [Fact]
    public async Task Execute_WhenRepositoryFails_PropagatesException()
    {
        var id = Guid.NewGuid();

        var repositoryMock = new Mock<ITransactionRepository>();

        repositoryMock
            .Setup(x => x.GetByIdAsync(
                id,
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Database error"));

        var useCase = new GetTransactionByIdUseCase(repositoryMock.Object);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            useCase.Execute(id));

        Assert.Equal("Database error", exception.Message);
    }

}

