using FinancialTransactionProcessing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTransactionProcessing.Application.Transactions
{
    public class CreateTransactionUseCase
    {
        public Transaction Execute(string sourceAccount, string destinationAccount,decimal amount,string currency)
        {
            return new Transaction(sourceAccount, destinationAccount, amount, currency);
        }
    }
}
