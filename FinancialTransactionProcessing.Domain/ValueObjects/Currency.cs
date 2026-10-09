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
                   "Currency must contain exactly three English letters.",
                   nameof(value));
            }
            Value = value;

        }
    }
}
