using QuizB.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace QuizB.Contract.IService
{
    public interface IServiceTransaction
    {
        public Result Transfer(string SourceCardNumber, string DestinationCardNumber, float Amount);
        public List<GetTrranDto> GetListOfTransactions(string CardNumber);
    }
}
