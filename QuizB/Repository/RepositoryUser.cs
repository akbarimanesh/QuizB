using Microsoft.EntityFrameworkCore;
using QuizB.Contract.IRepository;
using QuizB.DataBase;
using QuizB.Entity;
using QuizB.MyMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizB.Repository
{
    public class RepositoryUser : IRepositoryUser
    {
        private readonly AppDbContext appDbContext;
        public RepositoryUser()
        {
            appDbContext = new AppDbContext();
        }
        public Card BalanceDisplay(string numberCard)
        {
           return appDbContext.Cards.AsNoTracking().FirstOrDefault(x => x.CardNumber == numberCard && x.UserId==MemoryDb.CurrentCard.UserId);
            
           
        }

        public void ChangeCardPassword(string numberCard, string oldPassword, string newPassword)
        {
           var card= appDbContext.Cards.Where(x => x.CardNumber == numberCard && x.Password == oldPassword ).FirstOrDefault();
            
                card.Password = newPassword;
                appDbContext.SaveChanges();
           
            
            
        }

        public bool IsCardForUser(string numberCard)
        {
            return appDbContext.Cards.AsNoTracking().Any(x => x.CardNumber == numberCard && x.UserId == MemoryDb.CurrentCard.UserId);
        }
    }
}
