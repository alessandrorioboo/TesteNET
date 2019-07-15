using ITSingular.TesteNET.DataTransfer.Entityes;
using ITSingular.TesteNET.RabbitMq;
using System.Web.Http;

namespace TesteNet.API.Controllers
{
    public class MachineInformationController : ApiController
    {
        public void Post(MachineInformation machineInformation)
        {
            RabbitMqHelper.AddQueue(machineInformation);
        }
    }
}
