using FinancialTransactionProcessing.Api.Contracts;
using FinancialTransactionProcessing.Application.Transactions;
using FinancialTransactionProcessing.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FinancialTransactionProcessing.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {

        private readonly ILogger<TransactionsController> _logger;
        private readonly CreateTransactionUseCase _useCase;

        public TransactionsController(ILogger<TransactionsController> logger, CreateTransactionUseCase useCase)
        {
            _logger = logger;
            _useCase = useCase;
        }

        [HttpPost]
        public ActionResult<TransactionResponse> CreateTransaction(CreateTransactionRequest request)
        {
            var transaction = _useCase.Execute(request.SourceAccount, request.DestinationAccount, request.Amount, request.Currency);
            return Ok(new TransactionResponse()
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
