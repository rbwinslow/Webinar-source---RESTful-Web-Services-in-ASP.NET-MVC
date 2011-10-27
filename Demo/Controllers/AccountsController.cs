using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalFinanceRepository;

namespace Demo.Controllers
{
    public class AccountsController : ApplicationController
    {
        public ActionResult Index(int? UserId) {
            var user = ResolveUser.FromRequest(this.ControllerContext.HttpContext, UserId);
            if (user == null) {
                return new HttpNotFoundResult();
            }
            else {
                return Json(user.Accounts, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Show(int UserId, int Id) {
            var user = Users.GetById(UserId);
            var repositoryAccount = user.GetAccount(Id);

            return RespondTo(format => {
                format.Default = View(new Models.Account { User = user, AccountInfo = repositoryAccount });
                format.Json = () => Json(repositoryAccount, JsonRequestBehavior.AllowGet);
            });
        }

        public ActionResult Destroy(int UserId, int Id) {
            ActionResult result = null;

            switch (Users.GetById(UserId).DeleteAccount(Id)) {
                case AccountDeleteResult.Deleted:
                    result = Json(new { success = true });
                    break;

                case AccountDeleteResult.NoSuchAccount:
                    result = new HttpNotFoundResult();
                    break;
            }

            return result;
        }
    }
}
