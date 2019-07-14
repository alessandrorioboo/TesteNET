using ITSingular.TesteNET.DataTransfer.Entityes;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Text;

namespace ITSingular.TesteNET.RabbitMq
{
    public class RabbitMqHelper
    {


        //factory.UserName = "guest";
        //    factory.Password = "guest";
        //    factory.VirtualHost = "/";
        //    factory.HostName = "localhost";
        //    factory.Port = 5672;


        //private static string ExchangeName
        //{
        //    get
        //    {
        //        return "exchangeTesteNET";
        //    }
        //}

        //private static string QueueName
        //{
        //    get
        //    {
        //        return "queueTesteNET";
        //    }
        //}

        //private static string RoutingKey
        //{
        //    get
        //    {
        //        return "routingKeyTesteNET";
        //    }
        //}

        private static IConnection _rabbitMqConnection;
        public static IConnection RabbitMqConnection
        {
            get
            {
                if (_rabbitMqConnection == null)
                    _rabbitMqConnection = GetRabbitMqConnection();

                return _rabbitMqConnection;
            }
        }

        private static IConnection GetRabbitMqConnection()
        {
            ConnectionFactory factory = new ConnectionFactory();
            // "guest"/"guest" by default, limited to localhost connections
            factory.UserName = RMQConfiguration.UserNameRMQ;
            factory.Password = RMQConfiguration.PasswordRMQ;
            factory.VirtualHost = RMQConfiguration.VirtualHostRMQ;
            factory.HostName = RMQConfiguration.HostNameRMQ;
            factory.Port = RMQConfiguration.PortRMQ;

            return factory.CreateConnection();
        }

        private static IModel GetModel()
        {
            IModel model = RabbitMqConnection.CreateModel();

            model.ExchangeDeclare(RMQConfiguration.ExchangeName, ExchangeType.Direct);
            model.QueueDeclare(RMQConfiguration.QueueName, false, false, false, null);
            model.QueueBind(RMQConfiguration.QueueName, RMQConfiguration.ExchangeName, RMQConfiguration.RoutingKey, null);

            return model;
        }


        public static void AddQueue(MachineInformation machineInformation)
        {
            IModel model = GetModel();

            var jsonified = JsonConvert.SerializeObject(machineInformation);
            byte[] messageBodyBytes = Encoding.UTF8.GetBytes(jsonified);
            model.BasicPublish(RMQConfiguration.ExchangeName, RMQConfiguration.RoutingKey, null, messageBodyBytes);
        }

        public static IList<MachineInformation> GetAllQueues()
        {
            IList<MachineInformation> listMachineInformation = new List<MachineInformation>();

            IModel model = GetModel();
            var response = model.QueueDeclarePassive(RMQConfiguration.QueueName);
            // returns the number of messages in Ready state in the queue
            var messageCount = response.MessageCount;

            bool noAck = false;
            BasicGetResult result = null;
            do
            {
                result = model.BasicGet(RMQConfiguration.QueueName, noAck);

                if (result != null)
                {
                    IBasicProperties props = result.BasicProperties;
                    byte[] body = result.Body;
                    model.BasicAck(result.DeliveryTag, false);
                    var strBody = System.Text.Encoding.Default.GetString(body);
                    try
                    {
                        var machineInformation = JsonConvert.DeserializeObject<MachineInformation>(strBody);
                        if (machineInformation != null)
                        {
                            listMachineInformation.Add(machineInformation);
                        }
                    }
                    catch
                    { }
                }
            } while (result != null);

            return listMachineInformation;
        }
    }
}
