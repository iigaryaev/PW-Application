using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PW_Application.Models;
using Model.Database;
using Model;
using System.Web.Security;

namespace PW_Application.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly PWDatabase db;
        private readonly AuthContext auth;

        public AccountController()
        {
            this.db = new PWDatabase();
            this.auth = new AuthContext(this.db);
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var account = this.auth.Authorize(model.Email, model.Password);
            if (account == null)
            {
                ModelState.AddModelError("Email", "Password incorrect or login not exists");
                return View(model);
            }

            FormsAuthentication.SetAuthCookie(model.Email, true);
            return this.RedirectToAction("Index", "Home");
        }
                
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var res = this.auth.Register(model.Email, model.Name, model.Password);
                if (res.IsSuccess)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("Email", res.MEssage);
            }
            
            return View(model);
        }
        
        
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}