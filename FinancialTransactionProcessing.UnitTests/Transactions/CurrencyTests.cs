using FinancialTransactionProcessing.Domain.Entities;
using FinancialTransactionProcessing.Domain.ValueObjects;

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
            var sourceAccountNumber = "IRR";
            var destinationAccountNumber = "1234567891235";
            var currency = "";
            Action transaction = () => new Transaction(sourceAccountNumber, destinationAccountNumber, 1000M, currency);
            Assert.Throws<ArgumentException>(transaction);

        }
        [Fact]
        public void CreateTransaction_InvalidCurrency_ShouldThrow()
        {

            var sourceAccountNumber = "IRR";
            var destinationAccountNumber = "1234567891235";
            var currency = "U";
            Action transaction = () => new Transaction(sourceAccountNumber, destinationAccountNumber, 1000M, currency);
            Assert.Throws<ArgumentException>(transaction);

        }
        [Fact]
        public void CreateCurrency_WithNonLetterCharacters_ShouldThrow()
        {

            var sourceAccountNumber = "IRR";
            var destinationAccountNumber = "1234567891235";
            var currency = "12345";
            Action transaction = () => new Transaction(sourceAccountNumber, destinationAccountNumber, 1000M, currency);
            Assert.Throws<ArgumentException>(transaction);

        }
        [Fact]
        public void CreateCurrency_WithNonLetterAndLetterCharacters_ShouldThrow()
        {

            var sourceAccountNumber = "IRR";
            var destinationAccountNumber = "1234567891235";
            var currency = "1U45";
            Action transaction = () => new Transaction(sourceAccountNumber, destinationAccountNumber, 1000M, currency);
            Assert.Throws<ArgumentException>(transaction);

        }

        [Fact]
        public void UpperCase_vs_LowerCaseCurrency_ShouldBeSame()
        {
            var lowCurrency = new Currency("usd");
            var upperCurrency = new Currency("USD");

            Assert.Equal(lowCurrency, upperCurrency);
        }
    }
}
