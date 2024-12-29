using App.Domain.Core;
using App.Domain.Core.Bank;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IServiceUser
{
    public Card BalanceDisplay(string numberCard);
    public void ChangeCardPassword(string numberCard, string oldPassword, string newPassword);
    public bool IsCardForUser(string numberCard);

}

