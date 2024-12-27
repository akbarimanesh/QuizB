using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Bank.Transaction.AppServices
{
    public interface ITransactionAppService
    {
        public Result Transfer(string SourceCardNumber, string DestinationCardNumber, float Amount);
        public List<GetTrranDto> GetListOfTransactions(string CardNumber);
        public void GenerateVerificationCode(string CardSouNumber);
        public bool IsVerificationCode(string CardSouNumber, string code);
    }
}
