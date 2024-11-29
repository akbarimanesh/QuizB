using QuizB.DataBase;
using QuizB.Dto;
using QuizB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizB.Contract.IRepository
{
    public interface IRepositoryTransaction
    {
        public void Transfer( string SourceCardNumber, string DestinationCardNumber, float Amount);
        public List<GetTrranDto> GetListOfTransactions(string CardNumber);
        public float SumTransactionCard(string CardNumber, float Amount);
        public Card GetCard(string CardNumber);
       

    }
}
