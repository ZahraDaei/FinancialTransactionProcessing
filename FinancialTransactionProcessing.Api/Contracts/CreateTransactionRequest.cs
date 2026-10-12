using System.ComponentModel.DataAnnotations;

namespace FinancialTransactionProcessing.Api.Contracts
{
    public class CreateTransactionRequest
    {
        [Required]
        [StringLength(13, MinimumLength = 13)]
        public string SourceAccount { get; set; }
        [Required]
        [StringLength(13, MinimumLength = 13)]
        public string DestinationAccount { get; set; }
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Amount { get; set; }
        [Required]
        public string Currency { get; set; }
    }
}