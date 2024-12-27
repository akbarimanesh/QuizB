using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core;
using App.Domain.Core.Bank;


    public interface IServiceCard
    {
        public Result Login(string cardNumber, string password);
        public string DisplayHolderName(string CardDesNumber);
    }

