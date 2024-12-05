using Microsoft.Data.SqlClient;
using QuizB.Contract.IRepository;
using QuizB.Contract.IService;
using QuizB.DataBase;
using QuizB.Dto;
using QuizB.Entity;
using QuizB.MyMemory;
using QuizB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;
using Transaction = QuizB.Entity.Transaction;

namespace QuizB.Service
{
    public class ServiceTransaction : IServiceTransaction
    {
        IRepositoryCard repositoryCard;
        IRepositoryTransaction repositoryTransaction;
        public ServiceTransaction()
        {
            repositoryTransaction = new RepositoryTransaction();
            repositoryCard = new RepositoryCard();
        }

       

        public List<GetTrranDto> GetListOfTransactions(string CardNumber)
        {
            if (CardNumber.Length != 16)
            {
                throw new Exception("The card number numberCard is not valid.");
            }
            if (!repositoryCard.IsActive(CardNumber))
            {
                throw new Exception("numberCard is blocked.");
            }
            if (!repositoryCard.IsCardExists(CardNumber))
            {
                throw new Exception("This card is not available..");
            }

            else 
            {
                if(repositoryTransaction.GetListOfTransactions(CardNumber) == null)
                {
                    throw new Exception("You do not have access to this card.");
                }
                else return repositoryTransaction.GetListOfTransactions(CardNumber);

            }
            
        }

        public bool IsVerificationCode(string CardSouNumber, string code)
        {
            var data= repositoryTransaction.ReadVerificationCode();
            if (data == null) { return false; }
            else
            {
                string[] lines = data.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                
               
                foreach (var item in lines)
                {
                    if (item is "") return false;
                    var dataCode = item.ToString().Split('-');
                    var cardSouNumber= dataCode[0];
                    var VerificationCode = dataCode[1];
                    var dateTime= DateTime.Parse(dataCode[2]);
                    var differenceDate = DateTime.Now - (DateTime)dateTime;
                    int minutes = (int)differenceDate.TotalMinutes;
                    if (cardSouNumber==CardSouNumber && VerificationCode==code && minutes <= 2)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public void GenerateVerificationCode(string CardSouNumber)
        {
            
            repositoryTransaction.GenerateVerificationCode(CardSouNumber);
        }
        public Result Transfer(string SourceCardNumber, string DestinationCardNumber, float Amount)

        {
            bool isSuccessful = false;
            float fee = 0;
            if (Amount > 1000)
            {
                fee = (float)(Amount * 0.015);

            }

            if (Amount <= 1000)
            {
                fee = (float)(Amount * 0.005);

            }
            if (SourceCardNumber.Length != 16 && DestinationCardNumber.Length != 16)
            {
                return new Result(false, "The card number SourceCardNumber or DestinationCardNumber is not valid.");
            }
            if (Amount <= 0)
            {
                return new Result(false, "The deposit amount must be greater than zero.");
            }

            if (repositoryTransaction.SumTransactionCard(MemoryDb.CurrentCard.CardNumber, Amount) + Amount > 250)
            {
                return new Result(false, "our transaction limit has been reached.");
            }
            if (!repositoryCard.IsActive(SourceCardNumber))
            {
                return new Result(false, "SourceCardNumber is blocked.");
            }
            if (!repositoryCard.IsActive(DestinationCardNumber))
            {
                return new Result(false, "DestinationCardNumber is blocked.");
            }

            if (MemoryDb.CurrentCard.Balance < Amount + fee)
            {
                return new Result(false, "There is not enough inventory.");
            }

            else
            {

              
                var cardSource = repositoryCard.GetCardSource(SourceCardNumber);


                cardSource.Balance =cardSource.Balance - Amount - fee;
                var cardSourceBalance = cardSource.Balance;
                repositoryCard.UpdateCardSource(SourceCardNumber,cardSourceBalance);
                var cardDes = repositoryCard.GetCardSource(DestinationCardNumber);
                try
                {
                    cardDes.Balance = cardDes.Balance + Amount;
                    var cardDesBalance = cardDes.Balance;
                    repositoryCard.UpdateCardDes(DestinationCardNumber,cardDesBalance);

                    isSuccessful = true;
                }
                catch (Exception ex)
                {
                    cardSource.Balance = cardSource.Balance + Amount + fee;
                    cardSourceBalance = cardSource.Balance;
                    repositoryCard.UpdateCardSource(SourceCardNumber, cardSourceBalance);
                    isSuccessful = false;
                    throw new Exception("Transer Money is Faild");
                }
                finally
                {
                    var trans = new Transaction
                    {
                        CardId = MemoryDb.CurrentCard.Id,
                        Amount = Amount,
                        SourceCardNumber = SourceCardNumber,
                        DestinationCardNumber = DestinationCardNumber,
                        isSuccessful = isSuccessful,
                        TransactionDate = DateTime.Now,

                    };

                    repositoryTransaction.Transfer(trans);


                }
                return new Result(true, "Do it successfully.");
            }
        
               

        }

       
    }
}
