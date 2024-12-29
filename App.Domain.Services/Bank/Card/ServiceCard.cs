using App.Domain.Core.Bank;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class ServiceCard : IServiceCard
    {
        IRepositoryCard repositoryCard;
       
        public ServiceCard()
        {
            repositoryCard = new RepositoryCard();
        }

    public bool CheckPassword(string CardNumber, string oldpassword)
    {
        return repositoryCard.CheckPassword(CardNumber, oldpassword);
    }

    public string DisplayHolderName(string CardDesNumber)
    {
        return repositoryCard.DisplayHolderName(CardDesNumber);
    }

    public Card GetCard(string CardNumber)
    {
       return repositoryCard.GetCard(CardNumber);
    }

    public Card GetCardDes(string DestinationCardNumber)
    {
        return repositoryCard.GetCardDes(DestinationCardNumber);
    }

    public Card GetCardSource(string SourceCardNumber)
    {
       return repositoryCard.GetCardSource(SourceCardNumber);
    }

    public bool IsActive(string CardNumber)
    {
        return repositoryCard.IsActive(CardNumber);
    }

    public bool IsCardExists(string CardNumber)
    {
        return repositoryCard.IsCardExists(CardNumber);
    }

    public void UpdateCard(string CardNumber)
    {
         repositoryCard.UpdateCard(CardNumber);
    }

    public void UpdateCardDes(string DestinationCardNumber, float CardDesBalance)
    {
       repositoryCard.UpdateCardDes(DestinationCardNumber, CardDesBalance);
    }

    public void UpdateCardSource(string SourceCardNumber, float CardSourceBalance)
    {
        repositoryCard.UpdateCardSource(SourceCardNumber, CardSourceBalance);
    }
}



