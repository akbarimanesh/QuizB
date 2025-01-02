using App.Domain.Core.Bank;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UserAppService : IUserAppService
{
    IServiceUser _serviceUser;
    IServiceCard _serviceCard;
    public UserAppService(IServiceUser serviceUser , IServiceCard serviceCard)
    {
       _serviceUser = serviceUser;
       _serviceCard = serviceCard;
    }

   

   

    public Card BalanceDisplay(string numberCard)
    {
        if (numberCard.Length != 16)
        {
            throw new Exception("The card number numberCard is not valid.");
        }
        if (!_serviceCard.IsActive(numberCard))
        {
            throw new Exception("numberCard is blocked.");
        }
        if (!_serviceCard.IsCardExists(numberCard))
        {
            throw new Exception("This card is not available..");
        }

        else
        {
            if (_serviceUser.BalanceDisplay(numberCard) == null)
            {
                throw new Exception("You do not have access to this card.");
            }
            else
                return _serviceUser.BalanceDisplay(numberCard);


        }
    }

    public Result ChangeCardPassword(string numberCard, string oldPassword, string newPassword)
    {
        if (numberCard.Length != 16)
        {
            return new Result(false, "The card number numberCard is not valid.");

        }
        if (!_serviceCard.IsActive(numberCard))
        {
            return new Result(false, "numberCard is blocked.");

        }
        if (!_serviceCard.IsCardExists(numberCard))
        {
            return new Result(false, "Card not available.");
        }
        if (!_serviceUser.IsCardForUser(numberCard))
        {
            return new Result(false, "You do not have access to this card.");
        }
        if (!_serviceCard.CheckPassword(numberCard, oldPassword))
        {
            return new Result(false, "The old password is incorrect.");
        }
        else
        {

            _serviceUser.ChangeCardPassword(numberCard, oldPassword, newPassword);
            return new Result(true, "Password changed successfully.");
        }


    }
}

