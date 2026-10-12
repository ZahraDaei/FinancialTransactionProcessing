using System.Text.RegularExpressions;

namespace FinancialTransactionProcessing.Domain.ValueObjects
{
    public class Currency
    {
        public string Value { get;}
        public Currency(string value)
        {
            if (string.IsNullOrWhiteSpace(value)|| value.Length != 3 || !Regex.IsMatch(value,@"^[a-zA-Z]{3}$") )
            {
                throw new ArgumentException(
                   "نرخ ارز بایستی حروف انگلیسی و طول سه کاراکتر باشد",
                   nameof(value));
            }
            Value = value;

        }

        public override bool Equals(object? obj)
        {
            if (obj is not Currency currency)
            {
                return false;
            }
            this.GetHashCode();
            return currency.Value == Value;
        }
    }
}
