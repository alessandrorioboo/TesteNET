using ITSingular.TesteNET.Data.Repository;
using ITSingular.TesteNET.DataTransfer.Entityes;
using ITSingular.TesteNET.RabbitMq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Timers;


namespace ITSingular.TesteNET.Processer
{
    public partial class ProcesserService : ServiceBase
    {
        private static readonly MachineInformationRepository _machineInformationRepository = new MachineInformationRepository();
        private static readonly MachineInformationApplicationRepository _machineInformationApplicationRepository = new MachineInformationApplicationRepository();

        public ProcesserService()
        {
            InitializeComponent();
        }

        private static System.Timers.Timer _timerGetPersistMachineInformation;

        private static string eventSource = "TesteNet.Processer";
        private static string evebtLogName = "Application";

        protected override void OnStart(string[] args)
        {
            _timerGetPersistMachineInformation = new System.Timers.Timer();
            _timerGetPersistMachineInformation.Interval = 10 * 1000; // 10 seconds
            _timerGetPersistMachineInformation.Elapsed += new System.Timers.ElapsedEventHandler(timerGetPersistMachineInformation);
            _timerGetPersistMachineInformation.Start();
        }

        protected override void OnStop()
        {

        }

        private static void timerGetPersistMachineInformation(object sender, ElapsedEventArgs e)
        {
            bool teste = true;
            if (teste)
            {
                try
                {
                    _timerGetPersistMachineInformation.Stop();
                    var listaMachineInformation = RabbitMqHelper.GetAllQueues();
                    if (listaMachineInformation != null && listaMachineInformation.Count > 0)
                        AddMachineInformation(listaMachineInformation);
                }
                finally
                {
                    _timerGetPersistMachineInformation.Start();
                }
            }
        }

        private static void AddMachineInformation(IList<MachineInformation> listaMachineInformation)
        {
            try
            {
                foreach (var machineInformation in listaMachineInformation)
                {
                    Guid? idMachineInformationOld = _machineInformationRepository.GetIdByMachineName(machineInformation.MachineName);
                    if (idMachineInformationOld == null || idMachineInformationOld == Guid.Empty)
                    {
                        _machineInformationRepository.Add(machineInformation);
                    }
                    else
                    {
                        machineInformation.IdMachineInformation = idMachineInformationOld ?? Guid.Empty;
                        _machineInformationRepository.Update(machineInformation);
                    }

                    IList<MachineInformationApplication> listaMachineInformationApplication = new List<MachineInformationApplication>();

                    machineInformation.AppListInMachineClient.ToList().ForEach(p =>
                        listaMachineInformationApplication.Add(
                            new MachineInformationApplication
                            {
                                MachineInformationId = machineInformation.IdMachineInformation,
                                Application = p
                            }));

                    if (idMachineInformationOld != null)
                        _machineInformationApplicationRepository.RemoveByMachineInformationId(machineInformation.IdMachineInformation);
                    _machineInformationApplicationRepository.AddWithTransaction(listaMachineInformationApplication);
                };

                SaveEventLog("All Data updated successfully.", EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                var strLog = string.Format("Error update data.!\r\nException:\r\n{0}", ex.Message);
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