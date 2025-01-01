using App.Domain.Core.Bank;
using App.Domain.Core.Bank.Card.AppServices;
using Colors.Net;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.Mvc.Bank.Controllers
{
    public class CardController : Controller
    {
        private readonly ICardAppService cardAppService;
        public CardController()
        {
            cardAppService = new CardAppService();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        public IActionResult Login(string cardNumber, string password)
        {

            var result = cardAppService.Login(cardNumber, password);
            
            if (result.IsSuccess)
            {
                  HttpContext.Session.SetString("CardNumber", cardNumber);
                  return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewBag.ErrorMessage = result.IsMessage;
            }
           
            return View();


        }

        

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("CardNumber");
            return RedirectToAction("Login");
        }
    }
}
