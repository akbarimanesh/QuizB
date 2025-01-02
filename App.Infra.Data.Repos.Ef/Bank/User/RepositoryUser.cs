using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class RepositoryUser : IRepositoryUser
    {
        private readonly AppDbContext _appDbContext;

    public RepositoryUser(AppDbContext appDbContext)
    {
          _appDbContext = appDbContext;
    }

   
        public Card BalanceDisplay(string numberCard)
        {
           return _appDbContext.Cards.AsNoTracking().FirstOrDefault(x => x.CardNumber == numberCard && x.UserId==MemoryDb.CurrentCard.UserId);
            
           
        }

        public void ChangeCardPassword(string numberCard, string oldPassword, string newPassword)
        {
           var card= _appDbContext.Cards.Where(x => x.CardNumber == numberCard && x.Password == oldPassword ).FirstOrDefault();
            
                card.Password = newPassword;
                _appDbContext.SaveChanges();
           
            
            
        }

        public bool IsCardForUser(string numberCard)
        {
            return _appDbContext.Cards.AsNoTracking().Any(x => x.CardNumber == numberCard && x.UserId == MemoryDb.CurrentCard.UserId);
        }
    }

