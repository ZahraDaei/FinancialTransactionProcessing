using FinancialTransactionProcessing.Domain.ValueObjects;

namespace FinancialTransactionProcessing.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public AccountNumber SourceAccount { get; }
        public AccountNumber DestinationAccount { get; }
        public decimal Amount { get; private set; }
        public Currency Currency { get; private set; }
        public TransactionStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Transaction(string sourceAccount, string destinationAccount, decimal amount, string currency)
        {
            var sourceAccountNumber =new AccountNumber(sourceAccount);
            var destinationAccountNumber =new AccountNumber(destinationAccount) ;
            CheckForSourceAndDestinationAccountNumber(sourceAccountNumber, destinationAccountNumber);
   
            Id = Guid.NewGuid();
            SourceAccount = sourceAccountNumber;
            DestinationAccount = destinationAccountNumber;
            Amount = amount<=0 ? throw new ArgumentException("مبلغ نمی تواند منفی یا صفر باشد"):amount;
            Currency =new Currency(currency);
            Status = TransactionStatus.Pending;
            CreatedAt = DateTime.Now;
        }

        private void CheckForSourceAndDestinationAccountNumber(AccountNumber accountNumber1, AccountNumber accountNumber2)
        {
            if (accountNumber1==accountNumber2)
            {
                throw new ArgumentException("شماره حساب مبدا و مقصد نمی تواند یکسان باشد"); 
            }
        }

        public void MarkAsProcessing()
        {
            if (Status != TransactionStatus.Pending)
            {
                throw new InvalidOperationException("only pending transactions can be processed.");
            }
            Status = TransactionStatus.Processing;
        }

        public void MarkAsCompleted()
        {
            if (Status != TransactionStatus.Processing)
            {
                throw new InvalidOperationException("Only processing transactions can be completed");
            }
            Status = TransactionStatus.Completed;
        }

        public void MarkAsFailed()
        {
            if (Status != TransactionStatus.Processing)
            {
                throw new InvalidOperationException("Only processing transactions can fail. ");
            }
            Status = TransactionStatus.Failed;
        }


    }
}
