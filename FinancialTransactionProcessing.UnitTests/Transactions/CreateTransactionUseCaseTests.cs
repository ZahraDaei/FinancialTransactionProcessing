//using FinancialTransactionProcessing.Application.Transactions;
//using FinancialTransactionProcessing.Domain.Entities;
//using FinancialTransactionProcessing.Domain.ValueObjects;
//using Xunit;

//namespace FinancialTransactionProcessing.UnitTests.Transactions
//{
//    public class CreateTransactionUseCaseTests
//    {
//        [Fact]
//        public void Execute_WithValidInput_ShouldCreatePendingTransaction()
//        {
//            var transactionExecuter = new CreateTransactionUseCase();
//            var sourceAcc ="4567891234567";
//            var destAcc ="1234567893214";
//            var amount =1000M;
//            var currency ="IRI";


//            var transaction=transactionExecuter.Execute(sourceAcc, destAcc,amount , currency);

        
//            Assert.Equal(TransactionStatus.Pending,transaction.Status);
//            Assert.Equal(amount,transaction.Amount);
//            Assert.Equal(sourceAcc, transaction.SourceAccount.Value);
//            Assert.Equal(destAcc, transaction.DestinationAccount.Value);
//            Assert.Equal(currency, transaction.Currency.Value);
//        }
//    }
//}
