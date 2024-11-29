using QuizB.Contract.IRepository;
using QuizB.Contract.IService;
using QuizB.Entity;
using QuizB.MyMemory;
using QuizB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizB.Service
{
    public class ServiceCard : IServiceCard
    {
        IRepositoryCard repositoryCard;
        private int _failedCount = 0;
        public ServiceCard()
        {
            repositoryCard = new RepositoryCard();
        }


        public Result Login(string cardNumber, string password)
        {
            if (cardNumber.Length != 16)
            {
                return new Result(false, "");
            }

            var card = repositoryCard.GetCard(cardNumber);

            if (card == null || !card.IsActive)
            {
                return new Result(false, "");
            }

            if (card.Password == password)
            {
                MemoryDb.CurrentCard = card;
                _failedCount = 0;
                return new Result(true, "");
            }
            else
            {
                _failedCount++;

                if (_failedCount >= 3)
                {
                    card.IsActive = false;
                    repositoryCard.UpdateCard(cardNumber);
                }

                return new Result(false, "");
            }
        }

        public ServiceCard(IRepositoryCard repositoryCard, int failedCount)
        {
            this.repositoryCard = repositoryCard;
            _failedCount = failedCount;
        }
    }
}

