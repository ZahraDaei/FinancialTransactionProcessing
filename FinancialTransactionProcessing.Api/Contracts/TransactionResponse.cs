namespace FinancialTransactionProcessing.Api.Contracts
{
    public class TransactionResponse
    {
        public Guid Id { get; set; }
        public string SourceAccount { get; set; }
        public string DestinationAccount { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
