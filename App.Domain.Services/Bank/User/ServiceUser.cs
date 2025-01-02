
using App.Domain.Core.Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class ServiceUser : IServiceUser
    { 
        IRepositoryUser _RepositoryUser;

    public ServiceUser(IRepositoryUser repositoryUser)
    {
        _RepositoryUser = repositoryUser;
    }

    public Card BalanceDisplay(string numberCard)
    {
        return _RepositoryUser.BalanceDisplay(numberCard);
    }

    public void ChangeCardPassword(string numberCard, string oldPassword, string newPassword)
    {
        _RepositoryUser.ChangeCardPassword(numberCard, oldPassword, newPassword);
    }

    public bool IsCardForUser(string numberCard)
    {
        return _RepositoryUser.IsCardForUser(numberCard);
    }
}

