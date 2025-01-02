using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class RepositoryCard : IRepositoryCard
    {
        private readonly AppDbContext _appDbContext;

    public RepositoryCard(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

   

        public Card GetCard(string CardNumber)
        {
           return _appDbContext.Cards.AsNoTracking().FirstOrDefault(x => x.CardNumber == CardNumber);
        }

        public bool IsActive(string CardNumber)
        {
            return _appDbContext.Cards.AsNoTracking().Any(x => x.CardNumber == CardNumber);
        }

        public bool IsCardExists(string CardNumber)
        {
            return _appDbContext.Cards.AsNoTracking().Any(x => x.CardNumber == CardNumber);
        }

        public bool CheckPassword(string CardNumber, string oldpassword)
        {
            
            return _appDbContext.Cards.AsNoTracking().Any(x => x.CardNumber == CardNumber && x.Password == oldpassword);
            
            
           
        }

        public void UpdateCard(string CardNumber)
        {
            var card = _appDbContext.Cards.FirstOrDefault(p => p.CardNumber == CardNumber);
            card.IsActive = false;
           _appDbContext.SaveChanges();
        }

        public Card GetCardSource(string SourceCardNumber)
        {
           return _appDbContext.Cards.AsNoTracking().FirstOrDefault(x => x.CardNumber == SourceCardNumber && MemoryDb.CurrentCard.UserId == x.UserId);
        }

        public Card GetCardDes(string DestinationCardNumber)
        {
            return _appDbContext.Cards.AsNoTracking().FirstOrDefault(x => x.CardNumber == DestinationCardNumber);
        }

        public void UpdateCardSource(string SourceCardNumber,float  CardSourceBalance)
        {
            var card= _appDbContext.Cards.FirstOrDefault(p => p.CardNumber == SourceCardNumber);
            card.Balance = CardSourceBalance;

            _appDbContext.SaveChanges();
        }

        public void UpdateCardDes(string DestinationCardNumber,float CardDesBalance)
        {
            var card = _appDbContext.Cards.FirstOrDefault(p => p.CardNumber == DestinationCardNumber);
            card.Balance = CardDesBalance;
           _appDbContext.SaveChanges();
        }
        public string DisplayHolderName(string CardDesNumber)
        {
            var card = _appDbContext.Cards.AsNoTracking().FirstOrDefault(x => x.CardNumber == CardDesNumber);
            return card.HolderName;
        }
    }

