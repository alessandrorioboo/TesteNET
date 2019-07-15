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
    public class DashboardController : Controller
    {
        private readonly DashboardAppService _application = new DashboardAppService();

        private string uriAPI = string.Empty;
        private string URIAPI
        {
            get
            {
                if (string.IsNullOrEmpty(uriAPI))
                {
                    uriAPI = ConfigurationManager.AppSettings["URIAPI"];
                }

                return uriAPI;
            }
        }

        private HttpClient client = null;
        public HttpClient Client
        {
            get
            {
                if (client == null)
                {
                    client = new HttpClient();
                    client.BaseAddress = new Uri(URIAPI);
                    client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                }

                return client;
            }
        }
        
        public ActionResult Index(int? limitTimeMinutes = null)
        {
            var dashboardViewModelResult = GetModel(limitTimeMinutes);
            return View(dashboardViewModelResult);
        }

        private DashboardViewModel GetModel(int? limitTimeMinutes)
        {
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            if (limitTimeMinutes != null)
            {
                var uri = string.Format("api/Dashboard?limitTimeMinutes={0}", limitTimeMinutes);

                HttpResponseMessage response = Client.GetAsync(uri).Result;

                if (response.IsSuccessStatusCode)
                {
                    var dataJSONString = response.Content.ReadAsStringAsync().Result;

                    dashboardViewModel = JsonConvert.DeserializeObject<DashboardViewModel>(dataJSONString);
                    dashboardViewModel.LimitTimeMinutes = limitTimeMinutes;
                }
            }

            return dashboardViewModel;
        }
    }
}