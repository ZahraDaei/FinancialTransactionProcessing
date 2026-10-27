
using FinancialTransactionProcessing.Application.Abstractions.Repositories;
using FinancialTransactionProcessing.Application.Common.Models;
using FinancialTransactionProcessing.Domain.Entities;

namespace FinancialTransactionProcessing.Application.Transactions;

public class GetAllTransactionsUseCase
{
    private readonly ITransactionRepository _transactionRepository;

    public GetAllTransactionsUseCase(
        ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<PagedResult<Transaction>> Execute(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        if (page < 1)
            throw new ArgumentOutOfRangeException(nameof(page));

        if (pageSize < 1 || pageSize > 100)
            throw new ArgumentOutOfRangeException(nameof(pageSize));

        var totalCount = await _transactionRepository.GetCountAsync(
            cancellationToken);

        var transactions = await _transactionRepository.GetAllAsync(
            page,
            pageSize,
            cancellationToken);

        return new PagedResult<Transaction>
        {
            Items = transactions,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
}
