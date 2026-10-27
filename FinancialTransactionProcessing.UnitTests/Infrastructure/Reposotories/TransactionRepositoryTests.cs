
using FinancialTransactionProcessing.Domain.Entities;
using FinancialTransactionProcessing.Infrastructure.Persistence;
using FinancialTransactionProcessing.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinancialTransactionProcessing.UnitTests.Infrastructure.Repositories;

public class TransactionRepositoryTests
{
    private static TransactionDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<TransactionDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new TransactionDbContext(options);
    }

    [Fact]
    public async Task AddAsync_SavesTransaction()
    {
        await using var context = CreateContext();
        var repository = new TransactionRepository(context);

        var transaction = new Transaction(
            "1234567891234",
            "4567891236541",
            156000,
            "IRR");

        await repository.AddAsync(transaction);

        var savedTransaction = await context.Transactions
            .FirstOrDefaultAsync(x => x.Id == transaction.Id);

        Assert.NotNull(savedTransaction);
        Assert.Equal(transaction.Id, savedTransaction.Id);
    }

    [Fact]
    public async Task GetByIdAsync_WhenTransactionExists_ReturnsTransaction()
    {
        await using var context = CreateContext();

        var transaction = new Transaction(
            "1234567891234",
            "4567891236541",
            156000,
            "IRR");

        context.Transactions.Add(transaction);
        await context.SaveChangesAsync();

        var repository = new TransactionRepository(context);

        var result = await repository.GetByIdAsync(transaction.Id);

        Assert.NotNull(result);
        Assert.Equal(transaction.Id, result.Id);
        Assert.Equal(transaction.Amount, result.Amount);
    }

    [Fact]
    public async Task GetByIdAsync_WhenTransactionDoesNotExist_ReturnsNull()
    {
        await using var context = CreateContext();
        var repository = new TransactionRepository(context);

        var result = await repository.GetByIdAsync(Guid.NewGuid());

        Assert.Null(result);
    }


    [Fact]
    public async Task AddAsync_WhenCancellationRequested_ThrowsOperationCanceledException()
    {
        await using var context = CreateContext();
        var repository = new TransactionRepository(context);

        var transaction = new Transaction(
            "1234567891234",
            "4567891236541",
            156000,
            "IRR");

        using var cancellationTokenSource = new CancellationTokenSource();
        await cancellationTokenSource.CancelAsync();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
            repository.AddAsync(
                transaction,
                cancellationTokenSource.Token));

        Assert.Equal(0, await context.Transactions.CountAsync());
    }


    [Fact]
    public async Task GetByIdAsync_DoesNotTrackEntity()
    {
        await using var context = CreateContext();

        var transaction = new Transaction(
            "1234567891234",
            "4567891236541",
            156000,
            "IRR");

        context.Transactions.Add(transaction);
        await context.SaveChangesAsync();

        context.ChangeTracker.Clear();

        var repository = new TransactionRepository(context);

        var result = await repository.GetByIdAsync(transaction.Id);

        Assert.NotNull(result);
        Assert.Equal(
            EntityState.Detached,
            context.Entry(result).State);
    }


    [Fact]
    public async Task GetByIdAsync_WhenCancellationRequested_ThrowsOperationCanceledException()
    {
        await using var context = CreateContext();
        var repository = new TransactionRepository(context);

        using var cancellationTokenSource = new CancellationTokenSource();
        await cancellationTokenSource.CancelAsync();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
            repository.GetByIdAsync(
                Guid.NewGuid(),
                cancellationTokenSource.Token));
    }


}
