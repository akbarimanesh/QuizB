
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Bank.Card.AppServices
{
    public interface ICardAppService
    {
        public Result Login(string cardNumber, string password);
        public string DisplayHolderName(string CardDesNumber);
    }
}
