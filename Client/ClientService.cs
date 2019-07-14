using ITSingular.TesteNET.DataTransfer;
using ITSingular.TesteNET.DataTransfer.Entityes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Management;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;


namespace ITSingular.TesteNET.Client
{
    public partial class ClientService : ServiceBase
    {
        public ClientService()
        {
            InitializeComponent();
        }

        private static System.Timers.Timer _timerGetMachineInformation;

        private static string eventSource = "TesteNet.Client";
        private static string evebtLogName = "Application";

        private const string _registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
        private static HttpClient client = new HttpClient();

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

        protected override void OnStart(string[] args)
        {
            if (!string.IsNullOrEmpty(URIAPI))
            {
                // Update port # in the following line.
                client.BaseAddress = new Uri(URIAPI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));


                //RunAsync().GetAwaiter().GetResult();

                _timerGetMachineInformation = new System.Timers.Timer();
                _timerGetMachineInformation.Interval = 60 * 1000; // 1 minute
                _timerGetMachineInformation.Elapsed += new System.Timers.ElapsedEventHandler(timerGetMachineInformation);
                _timerGetMachineInformation.Start();
            }
            else
            {
                var strLog = "You must configure the API URI in App.config.";
                SaveEventLog(strLog, EventLogEntryType.Error);
                throw new Exception(strLog);
            }
        }

        protected override void OnStop()
        {

        }

        private static void timerGetMachineInformation(object sender, ElapsedEventArgs e)
        {
            var utlDateTime = DateTime.UtcNow;
            IList<string> appListInMachineClient = new List<string>();


            ManagementObjectSearcher p = new ManagementObjectSearcher("SELECT * FROM Win32_Product");
            foreach (ManagementObject program in p.Get())
            {
                if (program != null && program.GetPropertyValue("Name") != null)
                {
                    appListInMachineClient.Add(program.GetPropertyValue("Name").ToString());
                }
            }


            //using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(_registry_key))
            //{
            //    foreach (string subkey_name in key.GetSubKeyNames())
            //    {
            //        using (RegistryKey subkey = key.OpenSubKey(subkey_name))
            //        {
            //            var displayName = subkey.GetValue("DisplayName");
            //            if (displayName != null)
            //                appListInMachineClient.Add(displayName.ToString());
            //        }
            //    }
            //}

            var machineInformation = new MachineInformation
            {
                MachineName = Environment.MachineName,
                DateTimeUTC = utlDateTime,
                AppListInMachineClient = appListInMachineClient
            };

            UpdateProductAsync(machineInformation);
        }

        private static async void UpdateProductAsync(MachineInformation appListInMachineClient)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(
                     "api/MachineInformation/MachineInformation", appListInMachineClient);

                SaveEventLog("Machine information sent successfully!", EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                var strLog = string.Format("Error sending machine information.!\r\nException:\r\n{0}", ex.Message);
                SaveEventLog(strLog, EventLogEntryType.Error);
            }
        }

        public static void SaveEventLog(string strLog, EventLogEntryType eventLogEntryType)
        {
            if (!EventLog.SourceExists(eventSource))
                EventLog.CreateEventSource(eventSource, evebtLogName);

            EventLog.WriteEntry(eventSource, strLog, eventLogEntryType);
        }

    }
}
