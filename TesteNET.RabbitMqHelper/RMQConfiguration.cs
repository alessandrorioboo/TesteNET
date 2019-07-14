using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSingular.TesteNET.RabbitMq
{
    public static class RMQConfiguration
    {
        private static string exchangeName = string.Empty;
        public static string ExchangeName
        {
            get
            {
                if (string.IsNullOrEmpty(exchangeName))
                {
                    exchangeName = ConfigurationManager.AppSettings["ExchangeName"];
                    if (string.IsNullOrEmpty(exchangeName))
                        exchangeName = "exchangeTesteNET";
                }

                return exchangeName;
            }
        }

        private static string queueName = string.Empty;
        public static string QueueName
        {
            get
            {
                if (string.IsNullOrEmpty(queueName))
                {
                    queueName = ConfigurationManager.AppSettings["QueueName"];
                    if (string.IsNullOrEmpty(queueName))
                        queueName = "queueTesteNET";
                }

                return queueName;
            }
        }

        private static string routingKey = string.Empty;
        public static string RoutingKey
        {
            get
            {
                if (string.IsNullOrEmpty(routingKey))
                {
                    routingKey = ConfigurationManager.AppSettings["RoutingKey"];
                    if (string.IsNullOrEmpty(routingKey))
                        routingKey = "routingKeyTesteNET";
                }

                return routingKey;
            }
        }

        private static string userNameRMQ = string.Empty;
        public static string UserNameRMQ
        {
            get
            {
                if (string.IsNullOrEmpty(userNameRMQ))
                {
                    userNameRMQ = ConfigurationManager.AppSettings["UserNameRMQ"];
                    if (string.IsNullOrEmpty(userNameRMQ))
                        userNameRMQ = "guest";
                }

                return userNameRMQ;
            }
        }

        private static string passwordRMQ = string.Empty;
        public static string PasswordRMQ
        {
            get
            {
                if (string.IsNullOrEmpty(passwordRMQ))
                {
                    passwordRMQ = ConfigurationManager.AppSettings["PasswordRMQ"];
                    if (string.IsNullOrEmpty(passwordRMQ))
                        passwordRMQ = "guest";
                }

                return passwordRMQ;
            }
        }

        private static string virtualHostRMQ = string.Empty;
        public static string VirtualHostRMQ
        {
            get
            {
                if (string.IsNullOrEmpty(virtualHostRMQ))
                {
                    virtualHostRMQ = ConfigurationManager.AppSettings["VirtualHostRMQ"];
                    if (string.IsNullOrEmpty(virtualHostRMQ))
                        virtualHostRMQ = "/";
                }

                return virtualHostRMQ;
            }
        }

        private static string hostNameRMQ = string.Empty;
        public static string HostNameRMQ
        {
            get
            {
                if (string.IsNullOrEmpty(hostNameRMQ))
                {
                    hostNameRMQ = ConfigurationManager.AppSettings["HostName"];
                    if (string.IsNullOrEmpty(hostNameRMQ))
                        hostNameRMQ = "localhost";
                }

                return hostNameRMQ;
            }
        }

        private static int portRMQ = -1;
        public static int PortRMQ
        {
            get
            {
                if (portRMQ == -1)
                {
                    var portRMQStr = ConfigurationManager.AppSettings["PortRMQ"];
                    if (!Int32.TryParse(portRMQStr, out portRMQ))
                        portRMQ = 5672;
                    //if (string.IsNullOrEmpty(portRMQStr))
                    //    portRMQ = 5672;
                    //else
                    //    if (!Int32.TryParse(portRMQStr, out portRMQ))
                    //        portRMQ = 5672;
                }

                return portRMQ;
            }
        }


    }
}
