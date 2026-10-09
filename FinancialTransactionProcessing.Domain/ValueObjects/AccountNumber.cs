namespace FinancialTransactionProcessing.Domain.ValueObjects
{
    public class AccountNumber
    {
        public AccountNumber(string accountNumber)
        {
            Value = !string.IsNullOrEmpty(accountNumber)
                && accountNumber.Length == 13
                && accountNumber.All(char.IsDigit) ? accountNumber : throw new ArgumentException();
        }

        public string Value { get; private set; }

        public override bool Equals(object? obj)
        {
            if (obj is not AccountNumber accountNumber)
            {
                return false;
            }
            this.GetHashCode();
            return accountNumber.Value == Value;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public static bool operator == (AccountNumber a, AccountNumber b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
        

            return a.Value == b.Value;
        }

        public static bool operator !=(AccountNumber a, AccountNumber b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return false;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return true;


            return a.Value != b.Value;
        }
    }
}       