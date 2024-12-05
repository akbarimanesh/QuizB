using Microsoft.EntityFrameworkCore;
using QuizB.Contract.IRepository;
using QuizB.DataBase;
using QuizB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizB.Repository
{
    public class RepositoryCard : IRepositoryCard
    {
        private readonly AppDbContext appDbContext;
        public RepositoryCard()
        {
            appDbContext = new AppDbContext();
        }

        public Card GetCard(string CardNumber)
        {
           return  appDbContext.Cards.AsNoTracking().FirstOrDefault(x => x.CardNumber == CardNumber);
        }

        public bool IsActive(string CardNumber)
        {
            return appDbContext.Cards.AsNoTracking().Any(x => x.CardNumber == CardNumber);
        }

        public bool IsCardExists(string CardNumber)
        {
            return appDbContext.Cards.AsNoTracking().Any(x => x.CardNumber == CardNumber);
        }

        public bool CheckPassword(string CardNumber, string oldpassword)
        {
            
            return appDbContext.Cards.AsNoTracking().Any(x => x.CardNumber == CardNumber && x.Password == oldpassword);
            
            
           
        }

        public void UpdateCard(string CardNumber)
        {
            var card = appDbContext.Cards.FirstOrDefault(p => p.CardNumber == CardNumber);
            card.IsActive = false;
            appDbContext.SaveChanges();
        }

        public Card GetCardSource(string SourceCardNumber)
        {
           return appDbContext.Cards.AsNoTracking().FirstOrDefault(x => x.CardNumber == SourceCardNumber);
        }

        public Card GetCardDes(string DestinationCardNumber)
        {
            return appDbContext.Cards.AsNoTracking().FirstOrDefault(x => x.CardNumber == DestinationCardNumber);
        }

        public void UpdateCardSource(string SourceCardNumber,float  CardSourceBalance)
        {
            var card= appDbContext.Cards.FirstOrDefault(p => p.CardNumber == SourceCardNumber);
            card.Balance = CardSourceBalance;
            
            appDbContext.SaveChanges();
        }

        public void UpdateCardDes(string DestinationCardNumber,float CardDesBalance)
        {
            var card = appDbContext.Cards.FirstOrDefault(p => p.CardNumber == DestinationCardNumber);
            card.Balance = CardDesBalance;
            appDbContext.SaveChanges();
        }
        public string DisplayHolderName(string CardDesNumber)
        {
            var card = appDbContext.Cards.AsNoTracking().FirstOrDefault(x => x.CardNumber == CardDesNumber);
            return card.HolderName;
        }
    }
}
