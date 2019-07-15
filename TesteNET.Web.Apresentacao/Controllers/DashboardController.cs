using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteNET.Application;
using TesteNET.Application.ViewModel;

namespace TesteNET.Web.Apresentacao.Controllers
{
    public class DashboardController : Controller
    {
        private readonly MachineInformationAppService _application = new MachineInformationAppService();

        public ActionResult Index()
        {
            var dashboardViewModel = _application.GetDashboardData(5);
            return View(dashboardViewModel);
        }
   }
}