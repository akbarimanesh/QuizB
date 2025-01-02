using App.Domain.Core.Bank;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class ServiceCard : IServiceCard
    {
        IRepositoryCard _repositoryCard;

    public ServiceCard(IRepositoryCard repositoryCard)
    {
        _repositoryCard = repositoryCard;
    }

    public bool CheckPassword(string CardNumber, string oldpassword)
    {
        return _repositoryCard.CheckPassword(CardNumber, oldpassword);
    }

    public string DisplayHolderName(string CardDesNumber)
    {
        return _repositoryCard.DisplayHolderName(CardDesNumber);
    }

    public Card GetCard(string CardNumber)
    {
       return _repositoryCard.GetCard(CardNumber);
    }

    public Card GetCardDes(string DestinationCardNumber)
    {
        return _repositoryCard.GetCardDes(DestinationCardNumber);
    }

    public Card GetCardSource(string SourceCardNumber)
    {
       return _repositoryCard.GetCardSource(SourceCardNumber);
    }

    public bool IsActive(string CardNumber)
    {
        return _repositoryCard.IsActive(CardNumber);
    }

    public bool IsCardExists(string CardNumber)
    {
        return _repositoryCard.IsCardExists(CardNumber);
    }

    public void UpdateCard(string CardNumber)
    {
        _repositoryCard.UpdateCard(CardNumber);
    }

    public void UpdateCardDes(string DestinationCardNumber, float CardDesBalance)
    {
        _repositoryCard.UpdateCardDes(DestinationCardNumber, CardDesBalance);
    }

    public void UpdateCardSource(string SourceCardNumber, float CardSourceBalance)
    {
        _repositoryCard.UpdateCardSource(SourceCardNumber, CardSourceBalance);
    }
}



