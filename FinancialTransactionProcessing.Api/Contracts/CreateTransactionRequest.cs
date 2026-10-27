using System.ComponentModel.DataAnnotations;

namespace FinancialTransactionProcessing.Api.Models;

public class CreateTransactionRequest
{
    [Required]
    [StringLength(13, MinimumLength = 13)]
    [RegularExpression(@"^\d{13}$")]
    public string SourceAccount { get; set; } = string.Empty;

    [Required]
    [StringLength(13, MinimumLength = 13)]
    [RegularExpression(@"^\d{13}$")]
    public string DestinationAccount { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "9999999999999999")]
    public decimal Amount { get; set; }

    [Required]
    [RegularExpression(@"^[A-Za-z]{3}$")]
    public string Currency { get; set; } = "IRR";
}