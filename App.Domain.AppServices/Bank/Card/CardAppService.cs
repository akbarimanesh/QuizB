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
        public CardAppService()
        {
           SerCard = new ServiceCard();
        }
        public string DisplayHolderName(string CardDesNumber)
        {
            throw new NotImplementedException();
        }

        public Result Login(string cardNumber, string password)
        {
            throw new NotImplementedException();
        }

       
    }

