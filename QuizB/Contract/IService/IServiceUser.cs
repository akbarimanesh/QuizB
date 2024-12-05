using QuizB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizB.Contract.IService
{
    public interface IServiceUser
    {
        public Card BalanceDisplay(string numberCard);
        public Result ChangeCardPassword(string numberCard, string oldPassword, string newPassword);

    }
}
