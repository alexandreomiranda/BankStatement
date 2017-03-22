using BankStatement.DAL;
using BankStatement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankStatement.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            DALBankStatement DAL = new DALBankStatement();

            ViewBag.ID = new SelectList(new DALBankStatement().GetAccounts(), "ID", "Number");
            return View();
        }

        [HttpPost]
        public ActionResult Index(Guid id, DateTime initialDate)
        {
            DALBankStatement DAL = new DALBankStatement();
            var statement = from all in DAL.Get(id, initialDate)
                select new TransactionViewModel {
                    StartingDayBalance = all.StartingDayBalance,
                    ID = all.ID,
                    DateTransaction = all.DateTransaction,
                    Value = all.Value,
                    Description = all.Description,
                    Number = all.Number
                };
            ViewBag.ID = new SelectList(new DALBankStatement().GetAccounts(), "ID", "Number");
            return View(statement);
        }
    }
}