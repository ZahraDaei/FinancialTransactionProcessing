using FinancialTransactionProcessing.Domain.Entities;

namespace FinancialTransactionProcessing.UnitTests.Account
{
    public class AccountTests
    {
        [Fact]
        public void CreateAccount_WithNegativeBalance_ShouldThrow()
        {
            Action account =()=> new AccountNumber(-1000, "first", "last");
            Assert.Throws<ArgumentException>(account);
        }

        [Fact]
        public void Debit_WithValidAmount_ShouldDecreaseBalance()
        {
            var account = new AccountNumber(1000, "first", "last");
            account.Debit(800, "IRR");
            Assert.Equal(200, account.Balance);
        }

        [Fact]
        public void Debit_MoreThanBalance_ShouldThrowInsufficientFunds()
        {
            var account = new AccountNumber(1000, "first", "last");
            Action depit=()=>account.Debit(1800,"IRR");
            Assert.Throws<ArgumentException>(depit);
        }

        [Fact]
        public void Credit_WithValidAmount_ShouldIncreaseBalance()
        {
            var account = new AccountNumber(1000, "first", "last");
            account.Credit(100,"IRR");
            Assert.Equal(1100, account.Balance);        
        }

        [Fact]
        public void Debit_WithDifferentCurrency_ShouldThrow()
        {
            var account = new AccountNumber(1000, "first", "last");
            Action depit = () => account.Debit(100, "USD");
            Assert.Throws<ArgumentException>(depit);
        }
        [Fact]
        public void Debit_WithZeroOrNegativeAmount_ShouldThrow()
        {
            var account = new AccountNumber(1000, "first", "last");
            Action depitZero = () => account.Debit(0, "IRR");
            Action depitNegative = () => account.Debit(-100, "IRR");
            Assert.Throws<ArgumentException>(depitZero);
            Assert.Throws<ArgumentException>(depitNegative);
        }
    }
}
