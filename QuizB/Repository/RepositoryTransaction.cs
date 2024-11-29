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
            return appDbContext.Transactions.Where(x => x.Card.CardNumber==CardNumber).AsNoTracking()
                 .Select(x => new GetTrranDto()
                 {
                     Id = x.Id,
                     SourceCardNumber=x.SourceCardNumber,
                     DestinationCardNumber=x.DestinationCardNumber,
                     Amount=x.Amount,
                     TransactionDate=x.TransactionDate,
                     isSuccessful=x.isSuccessful,
                    

                 }).ToList();
        }

        public void Transfer(string SourceCardNumber, string DestinationCardNumber, float Amount)
        {
            
            var cardSource = GetCard(MemoryDb.CurrentCard.CardNumber);
            var transPrice= cardSource.Balance - Amount;
            var cardDes= GetCard(DestinationCardNumber);
            cardDes.Balance = transPrice;
            appDbContext.SaveChanges();
        }
    }
}
