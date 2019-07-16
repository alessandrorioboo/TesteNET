using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TesteNET.Application;
using TesteNET.Application.ViewModel;

namespace TesteNET.Web.Apresentacao.Controllers
{
    //[Authorize(Roles = "Administrators")]
    public class UserAuthorizationController : Controller
    {
        private readonly UserAuthorizationAppService _application = new UserAuthorizationAppService();

        public ActionResult Index()
        {
            var listUserAuthorizationViewModel = GetModel();
            return View(listUserAuthorizationViewModel);
        }

        private IList<UserAuthorizationViewModel> GetModel()
        {
            var listUserAuthorizationViewModel = _application.GetAllOrdered();
            return listUserAuthorizationViewModel;
        }

        [HttpPost]
        public ActionResult ChangeIsAdmin(Guid idUserAuthorization)
        {
            UserAuthorizationViewModel userAuthorizationViewModel = _application.GetByIdAsNoTracking(idUserAuthorization);
            userAuthorizationViewModel.IsAdmin = !userAuthorizationViewModel.IsAdmin;
            _application.Update(userAuthorizationViewModel);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}