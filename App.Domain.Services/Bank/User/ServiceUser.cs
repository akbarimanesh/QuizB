
using App.Domain.Core.Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class ServiceUser : IServiceUser
    { 
        IRepositoryUser RepositoryUser;
       
        public ServiceUser()
        {
            RepositoryUser = new RepositoryUser();
            
        }

    public Card BalanceDisplay(string numberCard)
    {
        return RepositoryUser.BalanceDisplay(numberCard);
    }

    public void ChangeCardPassword(string numberCard, string oldPassword, string newPassword)
    {
        RepositoryUser.ChangeCardPassword(numberCard, oldPassword, newPassword);
    }

    public bool IsCardForUser(string numberCard)
    {
        return RepositoryUser.IsCardForUser(numberCard);
    }
}

