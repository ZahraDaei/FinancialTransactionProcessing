using FinancialTransactionProcessing.Domain.Entities;

namespace FinancialTransactionProcessing.UnitTests.Transactions
{
    public class AccountNumberTests
    {

        [Fact]
        public void CreateTransaction_WithValidAccountNumber_ShouldSucced()
        {
            //Arange
            var sourceAccountNumber = "1234567891234";
            var destinationAccountNumber = "1234567891235";
            //Act
            var transaction = new Transaction(sourceAccountNumber, destinationAccountNumber, 1000M, "IR");

            //Asert
            Assert.Equal(destinationAccountNumber, transaction.DestinationAccount.Value);
            Assert.Equal(sourceAccountNumber, transaction.SourceAccount.Value);
        }

        [Fact]
        public void CreateTransaction_withSameSourceAndDestination_shouldThrow()
        {
            //Arrange
            var sourceAccountNumber = "1234567891234";
            var destinationAccountNumber = "1234567891234";

            //Act
            Action transaction = () => new Transaction(sourceAccountNumber, destinationAccountNumber, 1000M, "IR");

            //Assert
            Assert.Throws<ArgumentException>(transaction);
        }

        [Fact]
        public void CreateTransaction_withWrongAccount_shouldThrow()
        {
            //Arrange
            var sourceAccountNumber = "1234567891234677";
            var destinationAccountNumber = "1234567891234";

            //Act
            Action transaction = () => new Transaction(sourceAccountNumber, destinationAccountNumber, 1000M, "IR");

            //Assert
            Assert.Throws<ArgumentException>(transaction);
        }
    }
}
