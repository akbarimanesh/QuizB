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
    public class ServiceUser : IServiceUser
    { 
        IRepositoryUser RepositoryUser;
        IRepositoryCard repositoryCard;
        public ServiceUser()
        {
            RepositoryUser = new RepositoryUser();
            repositoryCard = new RepositoryCard();
        }
        public Card BalanceDisplay(string numberCard)
        {
            if (numberCard.Length != 16 )
            {
               throw new Exception( "The card number numberCard is not valid.");
            }
            if (!repositoryCard.IsActive(numberCard))
            {
                throw new Exception("numberCard is blocked.");
            }
            if (!repositoryCard.IsCardExists(numberCard))
            {
                throw new Exception("This card is not available..");
            }

            else
            {
                if(RepositoryUser.BalanceDisplay(numberCard)==null)
                {
                    throw new Exception("You do not have access to this card.");
                }
                else
                  return RepositoryUser.BalanceDisplay(numberCard);
               
               
            }
        }

        public Result ChangeCardPassword(string numberCard, string oldPassword, string newPassword)
        {
            if (numberCard.Length != 16)
            {
                return new Result(false, "The card number numberCard is not valid.");
                
            }
            if (!repositoryCard.IsActive(numberCard))
            {
                return new Result(false, "numberCard is blocked.");
                
            }
            if (!repositoryCard.IsCardExists(numberCard))
            {
                return new Result(false, "Card not available.");
            }
            if(!RepositoryUser.IsCardForUser(numberCard))
            {
                return new Result(false, "You do not have access to this card.");
            }
            if(!repositoryCard.CheckPassword(numberCard,oldPassword))
            {
                return new Result(false, "The old password is incorrect.");
            }
            else
            {

                RepositoryUser.ChangeCardPassword(numberCard, oldPassword, newPassword);
                return new Result(true, "Password changed successfully.");
            }
                
            
        }
    }
}
