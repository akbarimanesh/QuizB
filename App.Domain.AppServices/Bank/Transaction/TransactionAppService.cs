using App.Domain.Core.Bank;
using App.Domain.Core.Bank.Transaction.AppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.Bank.Transaction
{
    internal class TransactionAppService : ITransactionAppService
    {
        public void GenerateVerificationCode(string CardSouNumber)
        {
            throw new NotImplementedException();
        }

        public List<GetTrranDto> GetListOfTransactions(string CardNumber)
        {
            throw new NotImplementedException();
        }

        public bool IsVerificationCode(string CardSouNumber, string code)
        {
            throw new NotImplementedException();
        }

        public Result Transfer(string SourceCardNumber, string DestinationCardNumber, float Amount)
        {
            throw new NotImplementedException();
        }

        Result ITransactionAppService.Transfer(string SourceCardNumber, string DestinationCardNumber, float Amount)
        {
            throw new NotImplementedException();
        }
    }
}
