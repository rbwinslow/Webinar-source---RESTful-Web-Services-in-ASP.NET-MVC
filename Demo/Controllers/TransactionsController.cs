using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalFinanceRepository;

namespace Demo.Controllers
{
    public class TransactionsController : Controller
    {
        public ActionResult Index(int UserId, int AccountId)
        {
            return Json(Users.GetById(UserId).GetAccount(AccountId).Transactions, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(int UserId, int AccountId, Transaction transaction) {
            var account = Users.GetById(UserId).GetAccount(AccountId);
            int transactionId;
            account.AddTransaction(transaction, out transactionId);
            return Json(account.GetTransactionById(transactionId));
        }
    }
}
