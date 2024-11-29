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

       

        public bool IsCardExists(string CardNumber)
        {
            return appDbContext.Cards.AsNoTracking().Any(x => x.CardNumber == CardNumber);
        }

        public Card Login(string CardNumber, string password)
        {
            
           var card= appDbContext.Cards.Where(x => x.CardNumber == CardNumber && x.Password == password).AsNoTracking().FirstOrDefault();
            card.IsActive = true;
            return card;
           
        }

        public void UpdateCard(string CardNumber)
        {
            var card = appDbContext.Cards.FirstOrDefault(p => p.CardNumber == CardNumber);
            card.IsActive = false;
            appDbContext.SaveChanges();
        }
    }
}
