using App.Domain.Core;
using App.Domain.Core.Bank;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


    public interface IServiceTransaction
    {
        public Result Transfer(string SourceCardNumber, string DestinationCardNumber, float Amount);
        public List<GetTrranDto> GetListOfTransactions(string CardNumber);
        public void GenerateVerificationCode(string CardSouNumber);
        public bool IsVerificationCode(string CardSouNumber,string code);



    }

