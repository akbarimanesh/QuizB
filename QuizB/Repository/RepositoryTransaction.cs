using Microsoft.EntityFrameworkCore;
using QuizB.Contract.IRepository;
using QuizB.DataBase;
using QuizB.Dto;
using QuizB.Entity;
using QuizB.MyMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizB.Repository
{
    internal class RepositoryTransaction : IRepositoryTransaction
    {
        private readonly AppDbContext appDbContext;
        public RepositoryTransaction()
        {
            appDbContext = new AppDbContext();
        }





        public Card GetCard(string CardNumber)
        {
            return appDbContext.Cards.AsNoTracking().FirstOrDefault(x => x.CardNumber == CardNumber);
        }

        public List<GetTrranDto> GetListOfTransactions(string CardNumber)
        {
            return appDbContext.Transactions.Where(x => x.Card.CardNumber == CardNumber).AsNoTracking()
                 .Select(x => new GetTrranDto()
                 {
                     Id = x.Id,
                     SourceCardNumber = x.SourceCardNumber,
                     DestinationCardNumber = x.DestinationCardNumber,
                     Amount = x.Amount,
                     TransactionDate = x.TransactionDate,
                     isSuccessful = x.isSuccessful,


                 }).ToList();
        }

        

        public float SumTransactionCard(string CardNumber, float Amount)
        {
            var today = DateTime.Today;
            var sumTransaction = appDbContext.Transactions.Where(x => x.SourceCardNumber == CardNumber && x.TransactionDate.Date == today)
                 .Sum(x => x.Amount);
            MemoryDb.CurrentCard.SumTransaction = sumTransaction;
            return sumTransaction;
        }

        public void Transfer(string SourceCardNumber, string DestinationCardNumber, float Amount)
        {
            bool isSuccessful = false;
            var cardSource = appDbContext.Cards.FirstOrDefault(x => x.CardNumber == SourceCardNumber);


            cardSource.Balance = cardSource.Balance - Amount;
            var cardDes = appDbContext.Cards.FirstOrDefault(x => x.CardNumber == DestinationCardNumber);
            try
            {
                cardDes.Balance = cardDes.Balance + Amount;
                
                isSuccessful = true;
            }
            catch (Exception ex)
            {
                cardSource.Balance = cardSource.Balance + Amount;
                isSuccessful = false;
                throw new Exception("Transer Money is Faild");
            }
            finally
            {
                var trans = new Transaction
                {
                    CardId = MemoryDb.CurrentCard.Id,
                    Amount = Amount,
                    SourceCardNumber = SourceCardNumber,
                    DestinationCardNumber = DestinationCardNumber,
                    isSuccessful = isSuccessful,
                    TransactionDate = DateTime.Now,

                };
                appDbContext.Transactions.Add(trans);
            }

            appDbContext.SaveChanges();

        }


    }
}
