using Model.Database;
using Model.Database.Repository;
using PW_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PW_Application.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UnitOfWork database;

        //private readonly Account account;

        public HomeController()
        {
            this.database = new UnitOfWork(new PWDatabase());
            
        }

        public ActionResult Index()
        {

            if (this.User != null)
            {
                var account = this.database.AccountRepository.GetByLogin(this.User.Identity.Name);
                if (account == null)
                {
                    throw new Exception(string.Format("User [{0}] not found", this.User.Identity.Name));
                }

                var history = this.database.PaymentRepository.GetAllForUser(account.Id).ToList()
                    .Select(e => new PaymentHistoryItemViewModel
                    {
                        Ammount = e.Ammount,
                        Direction = e.CorrespondentUserId == account.Id ? PaymentDirection.In : PaymentDirection.Out,
                        OtherUserName = e.CorrespondentUserId != account.Id ? e.CorrespondentUser.UserName : e.SenderUser.UserName,
                        CreatedUtc = e.CreatedUtc,
                        State = e.State.Name
                    }).ToList();

                var vm = new AccountStateViewModel
                {
                    Name = account.UserName,
                    Balance = account.Balance,
                    History = history
                };

                return View(vm);
            }

            return null;
        }

        public ActionResult MakePayment(NewPayment payment)
        {
            if(payment.Ammount <= 0)
            {
                this.ModelState.AddModelError("Ammount", "Ammount should be more than zero");
                return this.PartialView("NewPaymentPartial", payment);
            }

            var account = this.database.AccountRepository.GetByLogin(this.User.Identity.Name);

            if (account.UserName == payment.CorrespondentName)
            {
                this.ModelState.AddModelError("CorrespondentName", "You can not transfer to yourself");
                return this.PartialView("NewPaymentPartial", payment);
            }

            var correspondent = this.database.AccountRepository.GetByName(payment.CorrespondentName);

            if (correspondent == null)
            {
                this.ModelState.AddModelError("CorrespondentName", "Correspondent not exists");
                return this.PartialView("NewPaymentPartial", payment);
            }

            if(account.Balance < payment.Ammount)
            {
                this.ModelState.AddModelError("Ammount", "Insufficient funds");
                return this.PartialView("NewPaymentPartial", payment);
            }

            this.database.PaymentRepository.Create(new Payment() { SenderUserId = account.Id, CorrespondentUserId = correspondent.Id, Ammount = payment.Ammount });
            this.database.Save();

            this.ViewData["PaymentRequested"] = true;
            return this.PartialView("NewPaymentPartial", payment);
        }

        public ActionResult GetHistory()
        {
            if(this.User == null)
            {
                throw new Exception("User undefined");
            }
            
            var account = this.database.AccountRepository.GetByLogin(this.User.Identity.Name);
            if (account == null)
            {
                throw new Exception(string.Format("User [{0}] not found", this.User.Identity.Name));
            }

            var history = this.database.PaymentRepository.GetAllForUser(account.Id).ToList()
                .Select(e => new PaymentHistoryItemViewModel
                {
                    Ammount = e.Ammount,
                    Direction = e.CorrespondentUserId == account.Id ? PaymentDirection.In : PaymentDirection.Out,
                    OtherUserName = e.CorrespondentUserId != account.Id ? e.CorrespondentUser.UserName : e.SenderUser.UserName,
                    CreatedUtc = e.CreatedUtc,
                    State = e.State.Name,
                    Id = e.Id
                }).ToList();
                
            return PartialView("HistoryPartial", history);
        }

        public ActionResult GetBalance()
        {
            if (this.User == null)
            {
                throw new Exception("User undefined");
            }

            var account = this.database.AccountRepository.GetByLogin(this.User.Identity.Name);
            if (account == null)
            {
                throw new Exception(string.Format("User [{0}] not found", this.User.Identity.Name));
            }

            return this.Json(new { Balance = account.Balance });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}