using FinancialTransactionProcessing.Api.Contracts;
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
        private readonly GetTransactionByIdUseCase _getUseCase;

        public TransactionsController(ILogger<TransactionsController> logger,
                                      CreateTransactionUseCase createUseCase,
                                      GetTransactionByIdUseCase getUseCase)
        {
            _logger = logger;
            _createUseCase = createUseCase;
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
    }
}
