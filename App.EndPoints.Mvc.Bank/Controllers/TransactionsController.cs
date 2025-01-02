using App.Domain.AppServices.Bank.Transaction;
using App.Domain.Core.Bank.Card.AppServices;
using App.Domain.Core.Bank.Transaction.AppServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace App.EndPoints.Mvc.Bank.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ITransactionAppService _transactionAppService;
        private readonly ICardAppService _cardAppService;
        public TransactionsController(ITransactionAppService transactionAppService , ICardAppService cardAppService)
        {
            _transactionAppService = transactionAppService;
            _cardAppService = cardAppService;
        }

       

       

        [HttpGet]
        public IActionResult ListTransactions()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Card");
            }
            return View();
        }
        [HttpPost]
        public IActionResult ListTransactions(string cardNumber)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Card");
            }
            try
            {
                var trans = _transactionAppService.GetListOfTransactions(cardNumber);
               


                return View("ListOfTransactions", trans);

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;


            }

            return View();
        }
        [HttpGet]
        public IActionResult Transfer()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Card");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Transfer(string sourceCardNumber,string destinationCardNumber,float amount)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Card");
            }
            try
            {

                var HolderName = _cardAppService.DisplayHolderName(destinationCardNumber);
                TempData["HolderName"] = HolderName;
                TempData["Amount"] =amount.ToString();
                TempData["SourceCardNumber"] = sourceCardNumber;

                TempData["DestinationCardNumber"] = destinationCardNumber;
                
                return RedirectToAction("Confirmation");
                
                              
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;


            }

            return View();


        }
        
        public IActionResult Confirmation(string action)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Card");
            }
            try
            {
                if (action == "cancel")
                    return RedirectToAction("Index", "Home");
                else if (action == "submit")
                    return RedirectToAction("SendCode");
            }

            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;


            }

            return View();
        }
        [HttpGet]
        public IActionResult SendCode()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Card");
            }
            if (TempData["SourceCardNumber"] != null)
            {
                string sourceCardNumber = TempData["SourceCardNumber"].ToString();
                _transactionAppService.GenerateVerificationCode(sourceCardNumber);
            }
            
            
            return RedirectToAction("Code");

        }
        [HttpGet]
        public IActionResult Code()
        {

            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Card");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Code( string code)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Card");
            }


            try
            {
                if (TempData["SourceCardNumber"] != null)
                {
                    string sourceCardNumber = TempData["SourceCardNumber"].ToString();
                    if (_transactionAppService.IsVerificationCode(sourceCardNumber, code))
                    {
                        return RedirectToAction("TransferMoney");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Your code has expired.";
                    }
                    return View();
                }
                

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;


            }

            return View();

        }
        
        public IActionResult TransferMoney()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Card");
            }
            
                string sourceCardNumber = TempData["SourceCardNumber"].ToString();

               string destinationCardNumber = TempData["DestinationCardNumber"].ToString();
                      
              float amount=Convert.ToSingle(TempData["Amount"]);

            var result = _transactionAppService.Transfer(sourceCardNumber, destinationCardNumber, amount);
            if (result.IsSuccess)
            {
                ViewBag.SuccessMessage = result.IsMessage;

            }
            else
            {
                ViewBag.ErrorMessage = result.IsMessage;
            }
            return View();
           
        }
        private bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("CardNumber"));
        }
    }
}
