using App.Domain.Core.Bank;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UserAppService : IUserAppService
{
    IServiceUser serviceUser;
    IServiceCard serviceCard;
    public UserAppService()
    {
        serviceCard= new ServiceCard();
        serviceUser= new ServiceUser();
    }
    public Card BalanceDisplay(string numberCard)
    {
        if (numberCard.Length != 16)
        {
            throw new Exception("The card number numberCard is not valid.");
        }
        if (!serviceCard.IsActive(numberCard))
        {
            throw new Exception("numberCard is blocked.");
        }
        if (!serviceCard.IsCardExists(numberCard))
        {
            throw new Exception("This card is not available..");
        }

        else
        {
            if (serviceUser.BalanceDisplay(numberCard) == null)
            {
                throw new Exception("You do not have access to this card.");
            }
            else
                return serviceUser.BalanceDisplay(numberCard);


        }
    }

    public Result ChangeCardPassword(string numberCard, string oldPassword, string newPassword)
    {
        if (numberCard.Length != 16)
        {
            return new Result(false, "The card number numberCard is not valid.");

        }
        if (!serviceCard.IsActive(numberCard))
        {
            return new Result(false, "numberCard is blocked.");

        }
        if (!serviceCard.IsCardExists(numberCard))
        {
            return new Result(false, "Card not available.");
        }
        if (!serviceUser.IsCardForUser(numberCard))
        {
            return new Result(false, "You do not have access to this card.");
        }
        if (!serviceCard.CheckPassword(numberCard, oldPassword))
        {
            return new Result(false, "The old password is incorrect.");
        }
        else
        {

            serviceUser.ChangeCardPassword(numberCard, oldPassword, newPassword);
            return new Result(true, "Password changed successfully.");
        }


    }
}

