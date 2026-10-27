using FinancialTransactionProcessing.Api.Contracts;
using FinancialTransactionProcessing.Api.Models;
using FinancialTransactionProcessing.Application.Common.Models;
using FinancialTransactionProcessing.Application.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace FinancialTransactionProcessing.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {

        private readonly ILogger<TransactionsController> _logger;
        private readonly CreateTransactionUseCase _createUseCase;
        private readonly GetAllTransactionsUseCase _getAllUseCase;
        private readonly GetTransactionByIdUseCase _getUseCase;

        public TransactionsController(ILogger<TransactionsController> logger,
                                      CreateTransactionUseCase createUseCase,
                                      GetAllTransactionsUseCase getAllTransactionsUseCase,
                                      GetTransactionByIdUseCase getUseCase)
        {
            _logger = logger;
            _createUseCase = createUseCase;
            _getAllUseCase = getAllTransactionsUseCase;
            _getUseCase = getUseCase;
        }


        [HttpPost]
        public async Task<ActionResult<TransactionResponse>> CreateTransaction(
            CreateTransactionRequest request,
            CancellationToken cancellationToken)
        {
            var transaction = await _createUseCase.Execute(
                request.SourceAccount,
                request.DestinationAccount,
                request.Amount,
                request.Currency,
                cancellationToken);

            var response = new TransactionResponse
            {
                Id = transaction.Id,
                SourceAccount = transaction.SourceAccount.Value,
                DestinationAccount = transaction.DestinationAccount.Value,
                Amount = transaction.Amount,
                Currency = transaction.Currency.Value,
                Status = transaction.Status.ToString(),
                CreatedAt = transaction.CreatedAt
            };

            return base.CreatedAtAction(
                nameof(GetTransaction),
                new { id = transaction.Id },
                response);
        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TransactionResponse>> GetTransaction(
          Guid id,
          CancellationToken cancellationToken)
        {
            var transaction = await _getUseCase.Execute(id, cancellationToken);
            if (transaction is null)
            {
                return NotFound();
            }
            return Ok(new TransactionResponse
            {
                Amount = transaction.Amount,
                CreatedAt = transaction.CreatedAt,
                Currency = transaction.Currency.Value,
                DestinationAccount = transaction.DestinationAccount.Value,
                Id = transaction.Id,
                SourceAccount = transaction.SourceAccount.Value,
                Status = transaction.Status.ToString()
            });
        }



        [HttpGet]
        public async Task<ActionResult<PagedResult<TransactionResponse>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var result = await _getAllUseCase.Execute(
                page,
                pageSize,
                cancellationToken);

            var response = new PagedResult<TransactionResponse>
            {
                Page = result.Page,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount,
                Items = result.Items.Select(transaction => new TransactionResponse
                {
                    Id = transaction.Id,
                    SourceAccount = transaction.SourceAccount.Value,
                    DestinationAccount = transaction.DestinationAccount.Value,
                    Amount = transaction.Amount,
                    Currency = transaction.Currency.Value,
                    Status = transaction.Status.ToString(),
                    CreatedAt = transaction.CreatedAt
                }).ToList()
            };

            return Ok(response);
        }



    }
}
