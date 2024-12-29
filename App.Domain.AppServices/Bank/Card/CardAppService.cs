using App.Domain.Core;
using App.Domain.Core.Bank;
using App.Domain.Core.Bank.Card.AppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class CardAppService : ICardAppService
    {
        IServiceCard SerCard;
        private int _failedCount = 0;
        public CardAppService()
        {
           SerCard = new ServiceCard();
        }
        public string DisplayHolderName(string CardDesNumber)
        {
             return SerCard.DisplayHolderName(CardDesNumber);
        }

    public Result Login(string cardNumber, string password)
    {
        if (cardNumber.Length != 16)
        {
            return new Result(false, "The card is not valid.");
        }

        var card = SerCard.GetCard(cardNumber);

        if (card == null || !card.IsActive)
        {
            return new Result(false, "Card is not active.");
        }

        if (card.Password == password)
        {
            MemoryDb.CurrentCard = card;
            _failedCount = 0;
            return new Result(true, "Welcome.");
        }
        else
        {
            _failedCount++;

            if (_failedCount >= 3)
            {
                card.IsActive = false;
                SerCard.UpdateCard(cardNumber);
                return new Result(false, "Card deactivated.");
            }

            return new Result(false, "pass invalid.");
        }

    }
       
    }

