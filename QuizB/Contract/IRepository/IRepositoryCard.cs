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
        public bool CheckPassword(string CardNumber, string oldpassword);
        public bool IsCardExists(string CardNumber);
        public Card GetCard(string CardNumber);
        public bool IsActive(string CardNumber);
        public Card GetCardSource(string SourceCardNumber);
        public Card GetCardDes(string DestinationCardNumber);
        public string DisplayHolderName(string CardDesNumber);
        public void UpdateCard(string CardNumber);
        public void UpdateCardSource(string SourceCardNumber,float CardSourceBalance);
        public void UpdateCardDes(string DestinationCardNumber,float CardDesBalance);
       
    }
}
