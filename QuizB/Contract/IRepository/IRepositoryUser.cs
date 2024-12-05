using QuizB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizB.Contract.IRepository
{
    public interface IRepositoryUser
    {
        public Card BalanceDisplay(string numberCard);
        public void ChangeCardPassword(string numberCard, string oldPassword ,string newPassword);
        public bool IsCardForUser(string numberCard);

    }
}
