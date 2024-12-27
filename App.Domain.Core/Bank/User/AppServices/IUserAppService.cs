using App.Domain.Core.Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public interface IUserAppService
    {
        public Card BalanceDisplay(string numberCard);
        public Result ChangeCardPassword(string numberCard, string oldPassword, string newPassword);
    }

