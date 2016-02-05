using FormAuthecicateionUserDataLab.Models;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FormAuthecicateionUserDataLab.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ViewResult SignIn()
        {
            return View("SignIn");
        }


        [HttpPost]
        public ActionResult SignIn(SignInModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("SignIn");
            }

            DateTime cookieIssuedDate = DateTime.Now;

            var ticket = new FormsAuthenticationTicket(0,
                model.UserName,
                cookieIssuedDate,
                cookieIssuedDate.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
                false,
                model.CustomUserData,
                FormsAuthentication.FormsCookiePath);

            string encryptedCookieContent = FormsAuthentication.Encrypt(ticket);

            var formsAuthenticationTicketCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedCookieContent)
            {
                Domain = FormsAuthentication.CookieDomain,
                Path = FormsAuthentication.FormsCookiePath,
                HttpOnly = true,
                Secure = FormsAuthentication.RequireSSL
            };

            System.Web.HttpContext.Current.Response.Cookies.Add(formsAuthenticationTicketCookie);

            return RedirectToAction("Home");
        }

        [Authorize]
        [HttpGet]
        public ViewResult Home()
        {
            return View("Home");
        }
    }
}