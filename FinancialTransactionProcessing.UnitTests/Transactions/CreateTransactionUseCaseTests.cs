
using FinancialTransactionProcessing.Application.Abstractions.Repositories;
using FinancialTransactionProcessing.Application.Transactions;
using FinancialTransactionProcessing.Domain.Entities;
using Moq;

namespace FinancialTransactionProcessing.UnitTests.Transactions;

public class CreateTransactionUseCaseTests
{

    [Fact]
    public async Task Execute_WithSameSourceAndDestination_ThrowsArgumentException()
    {
        var repositoryMock = new Mock<ITransactionRepository>();
        var useCase = new CreateTransactionUseCase(repositoryMock.Object);

        await Assert.ThrowsAsync<ArgumentException>(() =>
            useCase.Execute(
                "1234567891234",
                "1234567891234",
                156000,
                "IRR", CancellationToken.None));

        repositoryMock.Verify(x => x.AddAsync(
            It.IsAny<Transaction>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Execute_WithInvalidCurrency_ThrowsArgumentException()
    {
        var repositoryMock = new Mock<ITransactionRepository>();
        var useCase = new CreateTransactionUseCase(repositoryMock.Object);

        await Assert.ThrowsAsync<ArgumentException>(() =>
            useCase.Execute(
                "1234567891234",
                "4567891236541",
        156000,
                "IR", CancellationToken.None));

        repositoryMock.Verify(x => x.AddAsync(
            It.IsAny<Transaction>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Execute_WhenCancellationRequested_ThrowsOperationCanceledException()
    {
        var repositoryMock = new Mock<ITransactionRepository>();
        var useCase = new CreateTransactionUseCase(repositoryMock.Object);

        using var cancellationTokenSource = new CancellationTokenSource();
        await cancellationTokenSource.CancelAsync();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
            useCase.Execute(
                "1234567891234",
                "4567891236541",
                156000,
                "IRR",
                cancellationTokenSource.Token));

        repositoryMock.Verify(x => x.AddAsync(
            It.IsAny<Transaction>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }


    [Fact]
    public async Task Execute_WhenRepositoryFails_PropagatesException()
    {
        var repositoryMock = new Mock<ITransactionRepository>();

        repositoryMock
            .Setup(x => x.AddAsync(
                It.IsAny<Transaction>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Database error"));

        var useCase = new CreateTransactionUseCase(repositoryMock.Object);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            useCase.Execute(
                "1234567891234",
                "4567891236541",
                156000,
                "IRR"));

        Assert.Equal("Database error", exception.Message);
    }


}
