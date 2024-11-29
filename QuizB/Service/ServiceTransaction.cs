using Microsoft.Data.SqlClient;
using QuizB.Contract.IRepository;
using QuizB.Contract.IService;
using QuizB.Dto;
using QuizB.Entity;
using QuizB.MyMemory;
using QuizB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Transaction = QuizB.Entity.Transaction;

namespace QuizB.Service
{
    public class ServiceTransaction : IServiceTransaction
    {
        IRepositoryCard repositoryCard;
        IRepositoryTransaction repositoryTransaction;
        public ServiceTransaction()
        {
            repositoryTransaction = new RepositoryTransaction();
            repositoryCard = new RepositoryCard();
        }

       

        public List<GetTrranDto> GetListOfTransactions(string CardNumber)
        {
            if (repositoryCard.IsCardExists(CardNumber))
            {
                return  repositoryTransaction.GetListOfTransactions(CardNumber);

            }
            else
                return null;
        }

        public Result Transfer(string SourceCardNumber, string DestinationCardNumber, float Amount)
        
        {
            

            if (SourceCardNumber.Length != 16 && DestinationCardNumber.Length != 16)
            {
                return new Result(false, "The card number SourceCardNumber or DestinationCardNumber is not valid.");
            }
            if (Amount <= 0)
            {
                return new Result(false, "The deposit amount must be greater than zero.");
            }

           if(repositoryTransaction.SumTransactionCard(MemoryDb.CurrentCard.CardNumber,Amount)+Amount>MemoryDb.CurrentCard.maximumTransaction)
            {
                return new Result(false, "our transaction limit has been reached.");
            }
            if (MemoryDb.CurrentCard.Balance < Amount)
            {
                return new Result(false, "There is not enough inventory.");
            }

            else
            {
                repositoryTransaction.Transfer(SourceCardNumber, DestinationCardNumber, Amount);
             

                return new Result(true, "Do it successfully.");

            }
               

        }

       
    }
}
