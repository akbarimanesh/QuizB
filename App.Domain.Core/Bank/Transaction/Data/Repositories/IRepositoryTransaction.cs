

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public interface IRepositoryTransaction
    {
        public void Transfer(Transaction transaction);
       
        public List<GetTrranDto> GetListOfTransactions(string CardNumber);
        public float SumTransactionCard(string CardNumber, float Amount);
        public Card GetCard(string CardNumber);
        public void GenerateVerificationCode(string CardSouNumber);
        public string ReadVerificationCode();




    }

