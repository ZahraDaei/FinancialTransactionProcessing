namespace FinancialTransactionProcessing.Domain.Entities
{
    public class AccountNumber
    {
        public AccountNumber(decimal balance, string firstName, string lastName)
        {
            Balance = balance >= 0 ? balance : throw new ArgumentException("مقدار اعتبار نمی تواند منفی باشد");
            FirstName = firstName;
            LastName = lastName;
            Number = new Random().NextInt64(1111111111111, 99999999999999).ToString();
            Currency = "IRR";
        }
        public int Id { get; }
        public decimal Balance { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Number { get; }
        public string Currency { get; }

        public void Debit(decimal amount, string currency)
        {
            if (currency != Currency)
            {
                throw new ArgumentException("نوع ارز با نوع ارز حساب همسان نمی باشد.");
            }
            if (amount <= 0)
            {
                throw new ArgumentException("مقدار برداشت بایستی عددی بزرگتر از صفر باشد");
            }
            if (amount > Balance)
            {
                throw new ArgumentException("مقدار برداشت بایستی کوچکتر یا مساوی موجودی باشد");
            }
            Balance -= amount;

        }
        public void Credit(decimal amount,string currency)
        {
            if (currency != Currency)
            {
                throw new ArgumentException("نوع ارز با نوع ارز حساب همسان نمی باشد.");
            }
            if (amount <= 0)
            {
                throw new ArgumentException("مقدار واریز بایستی بزرگتر از صفر باشد");
            }
            Balance += amount;

        }
    }
}
