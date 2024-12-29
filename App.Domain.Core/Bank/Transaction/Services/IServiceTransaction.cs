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
         public void Transfer(Transaction transaction);
       
        public List<GetTrranDto> GetListOfTransactions(string CardNumber);
        public float SumTransactionCard(string CardNumber, float Amount);
        public Card GetCard(string CardNumber);
        public void GenerateVerificationCode(string CardSouNumber);
        public string ReadVerificationCode();



    }

