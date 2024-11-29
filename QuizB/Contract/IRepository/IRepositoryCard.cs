using QuizB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizB.Contract.IRepository
{
    public interface IRepositoryCard
    {
        public Card Login(string CardNumber, string password);
        public bool IsCardExists(string CardNumber);
        public Card GetCard(string CardNumber);
       

        public void UpdateCard(string CardNumber);
       
    }
}
