using App.Domain.Core.Bank;
using App.Domain.Core.Bank.Transaction.AppServices;
using App.Domain.Core.Bank.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace App.Domain.AppServices.Bank.Transaction
{
    public class TransactionAppService : ITransactionAppService
    {
        IServiceCard _serviceCard;
        IServiceTransaction _serviceTransaction;
        public TransactionAppService(IServiceCard serviceCard , IServiceTransaction serviceTransaction)
        {
            _serviceCard = serviceCard;
            _serviceTransaction = serviceTransaction;
        }

       

       

        public List<GetTrranDto> GetListOfTransactions(string CardNumber)
        {
            if (CardNumber.Length != 16)
            {
                throw new Exception("The card number numberCard is not valid.");
            }
            if (!_serviceCard.IsActive(CardNumber))
            {
                throw new Exception("numberCard is blocked.");
            }
            if (!_serviceCard.IsCardExists(CardNumber))
            {
                throw new Exception("This card is not available..");
            }

            else
            {
                if (_serviceTransaction.GetListOfTransactions(CardNumber) == null)
                {
                    throw new Exception("You do not have access to this card.");
                }
                else return _serviceTransaction.GetListOfTransactions(CardNumber);

            }

        }

        public bool IsVerificationCode(string CardSouNumber, string code)
        {
            var data = _serviceTransaction.ReadVerificationCode();
            if (data == null) { return false; }
            else
            {
                string[] lines = data.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);


                foreach (var item in lines)
                {
                    if (item is "") return false;
                    var dataCode = item.ToString().Split('-');
                    var cardSouNumber = dataCode[0];
                    var VerificationCode = dataCode[1];
                    var dateTime = DateTime.Parse(dataCode[2]);
                    var differenceDate = DateTime.Now - (DateTime)dateTime;
                    int minutes = (int)differenceDate.TotalMinutes;
                    if (cardSouNumber == CardSouNumber && VerificationCode == code && minutes <= 2)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public void GenerateVerificationCode(string CardSouNumber)
        {

            _serviceTransaction.GenerateVerificationCode(CardSouNumber);
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
            if (SourceCardNumber.Length != 16 || DestinationCardNumber.Length != 16)
            {
                return new Result(false, "The card number SourceCardNumber or DestinationCardNumber is not valid.");
            }
            if (Amount <= 0)
            {
                return new Result(false, "The deposit amount must be greater than zero.");
            }

            if (_serviceTransaction.SumTransactionCard(MemoryDb.CurrentCard.CardNumber, Amount) + Amount > 250)
            {
                return new Result(false, "our transaction limit has been reached.");
            }
            if (!_serviceCard.IsActive(SourceCardNumber))
            {
                return new Result(false, "SourceCardNumber is blocked.");
            }
            if (!_serviceCard.IsActive(DestinationCardNumber))
            {
                return new Result(false, "DestinationCardNumber is blocked.");
            }

            if (MemoryDb.CurrentCard.Balance < Amount + fee)
            {
                return new Result(false, "There is not enough inventory.");
            }

            else
            {


                var cardSource = _serviceCard.GetCardSource(SourceCardNumber);
                if (cardSource != null)
                {
                    cardSource.Balance = cardSource.Balance - Amount - fee;
                    var cardSourceBalance = cardSource.Balance;
                    _serviceCard.UpdateCardSource(SourceCardNumber, cardSourceBalance);
                    var cardDes = _serviceCard.GetCardDes(DestinationCardNumber);
                    try
                    {
                        cardDes.Balance = cardDes.Balance + Amount;
                        var cardDesBalance = cardDes.Balance;
                        _serviceCard.UpdateCardDes(DestinationCardNumber, cardDesBalance);

                        isSuccessful = true;
                    }
                    catch (Exception ex)
                    {
                        cardSource.Balance = cardSource.Balance + Amount + fee;
                        cardSourceBalance = cardSource.Balance;
                        _serviceCard.UpdateCardSource(SourceCardNumber, cardSourceBalance);
                        isSuccessful = false;
                        throw new Exception("Transer Money is Faild");
                    }
                    finally
                    {

                        var trans = new global::Transaction
                        {
                            CardId = MemoryDb.CurrentCard.Id,
                            Amount = Amount,
                            SourceCardNumber = SourceCardNumber,
                            DestinationCardNumber = DestinationCardNumber,
                            isSuccessful = isSuccessful,
                            TransactionDate = DateTime.Now,

                        };

                        _serviceTransaction.Transfer(trans);


                    }
                    return new Result(true, "Money Transfer Completed Successfully.");
                }

                else
                {
                    return new Result(false, "You do not have access to the SourceCardNumber .");


                }
            }



        }
    }
}
