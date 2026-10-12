using FinancialTransactionProcessing.Domain.Entities;

namespace FinancialTransactionProcessing.UnitTests.Transactions
{
    public class CurrencyTests
    {
        [Fact]
        public void CreateCurrency_WithValidCode_ShouldSucceed()
        {
            var sourceAccountNumber = "4567894561234";
            var destinationAccountNumber = "1234567891235";
            var currency = "USD";
            var transaction = new Transaction(sourceAccountNumber, destinationAccountNumber, 1000M, currency);
            Assert.Equal(currency, transaction.Currency.Value);
        }
        [Fact]
        public void CreateTransaction_EmptyCurrency_ShouldThrow()
        {
            var sourceAccountNumber = "IRI";
            var destinationAccountNumber = "1234567891235";
            var currency = "";
            Action transaction = () => new Transaction(sourceAccountNumber, destinationAccountNumber, 1000M, currency);
            Assert.Throws<ArgumentException>(transaction);

        }
        [Fact]
        public void CreateTransaction_InvalidCurrency_ShouldThrow()
        {

            var sourceAccountNumber = "IRI";
            var destinationAccountNumber = "1234567891235";
            var currency = "U";
            Action transaction = () => new Transaction(sourceAccountNumber, destinationAccountNumber, 1000M, currency);
            Assert.Throws<ArgumentException>(transaction);

        }
        [Fact]
        public void CreateCurrency_WithNonLetterCharacters_ShouldThrow()
        {

            var sourceAccountNumber = "IRI";
            var destinationAccountNumber = "1234567891235";
            var currency = "12345";
            Action transaction = () => new Transaction(sourceAccountNumber, destinationAccountNumber, 1000M, currency);
            Assert.Throws<ArgumentException>(transaction);

        }
        [Fact]
        public void CreateCurrency_WithNonLetterAndLetterCharacters_ShouldThrow()
        {

            var sourceAccountNumber = "IRI";
            var destinationAccountNumber = "1234567891235";
            var currency = "1U45";
            Action transaction = () => new Transaction(sourceAccountNumber, destinationAccountNumber, 1000M, currency);
            Assert.Throws<ArgumentException>(transaction);

        }
    }
}
