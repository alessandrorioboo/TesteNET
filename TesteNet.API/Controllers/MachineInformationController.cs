using ITSingular.TesteNET.DataTransfer.Entityes;
using ITSingular.TesteNET.RabbitMq;
using System.Web.Http;

namespace TesteNet.API.Controllers
{
    public class MachineInformationController : ApiController
    {
        //public void PostMachineInformation([FromBody]string machineName, [FromBody]DateTime dateTimeUTC, [FromBody]string appListInMachineClientCommaText)
        //{
        //    var xxxx = machineName;
        //}
        // GET api/values
        //public IEnumerable<MachineInformation> Get()
        //{
        //    var kkk = new List<string> { "app1", "app2", "app3", "app4" };
        //    var xxx = new MachineInformation[] { new MachineInformation { AppListInMachineClient = kkk, MachineName = "Name 1", DateTimeUTC = DateTime.Now },
        //                                         new MachineInformation { AppListInMachineClient = kkk, MachineName = "Name 2", DateTimeUTC = DateTime.Now } };

        //    //var xxx = new MachineInformation[] { new MachineInformation { AppListInMachineClient = new string[] { "app1", "app2", "app3" }, MachineName = "Name 1", DateTimeUTC = DateTime.Now },
        //    //                                     new MachineInformation { AppListInMachineClient = new string[] { "app4", "app5", "app6" }, MachineName = "Name 2", DateTimeUTC = DateTime.Now } };
        //    return xxx;
        //}

        //// GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        //public void Post(string value)
        //{
        //    var xxxx = value;
        //}

        public void PostMachineInformation(MachineInformation machineInformation)
        {
            RabbitMqHelper.AddQueue(machineInformation);
            //RabbitMqHelper.AddQueue(machineInformation);
            //RabbitMqHelper.AddQueue(machineInformation);
            //RabbitMqHelper.AddQueue(machineInformation);
            //RabbitMqHelper.AddQueue(machineInformation);


            //var xxx = RabbitMqHelper.GetAllQueues();    
        }

        //// PUT api/values/5
        //public void Put(int id, string value)
        //{
        //    var xxxx = id;
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}
