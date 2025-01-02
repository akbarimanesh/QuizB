using App.Domain.Core.Bank;
using App.Domain.Core.Bank.Card.AppServices;
using Colors.Net;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.Mvc.Bank.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {

            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Card");
            }
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(string cardNumber, string oldPassword,string newPassword)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Card");
            }
            try
            {
                var result = _userAppService.ChangeCardPassword(cardNumber, oldPassword, newPassword);
                if (result.IsSuccess)
                {

                    ViewBag.SuccessMessage = result.IsMessage;
                    return RedirectToAction("Login", "Card");

                }
                else
                {
                    ViewBag.ErrorMessage = result.IsMessage;
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;


            }





            return View();


        }
        [HttpGet]
        public IActionResult BalanceDisplay()
        {

            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Card");
            }
            return View();
        }
        [HttpPost]
        public IActionResult BalanceDisplay(string cardNumber)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Card");
            }
           

            try
            {
                var card = _userAppService.BalanceDisplay(cardNumber);
                ViewBag.SuccessMessage = card.Balance;
               
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
               

            }

            return View();

        }
        private bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("CardNumber"));
        }
    }
}
