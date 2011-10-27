using System.Web.Mvc;
using PersonalFinanceRepository;

namespace Demo.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult Index() {
            return Json(Users.All(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Show(int? id) {
            var user = ResolveUser.FromRequest(this.ControllerContext.HttpContext, id);
            if (user == null) {
                return new HttpNotFoundResult();
            }
            else {
                return Json(user, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Update(int Id, string Name, string Email) {
            ActionResult result = null;

            User user = new User {Id = Id, Name = Name, Email = Email};
            switch (Users.Update(user)) {
                case UserUpdateResult.Updated:
                    result = Json(Users.GetById(Id));
                    break;

                case UserUpdateResult.NoSuchUser:
                    result = new HttpNotFoundResult();
                    break;

                case UserUpdateResult.EmailInvalid:
                    result = new HttpStatusCodeResult(400);
                    break;

                case UserUpdateResult.NameOrEmailAlreadyTaken:
                    result = new HttpStatusCodeResult(409);
                    break;
            }

            return result;
        }
    }
}
