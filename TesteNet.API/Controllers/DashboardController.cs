using System.Web.Http;
using TesteNET.Application;
using TesteNET.Application.ViewModel;

namespace TesteNet.API.Controllers
{
    public class DashboardController : ApiController
    {
        private readonly DashboardAppService _appService = new DashboardAppService();

        public DashboardViewModel Get(int limitTimeMinutes)
        {
            var dashboardViewModel = _appService.GetDashboardData(limitTimeMinutes);
            return dashboardViewModel;
        }
    }
}
