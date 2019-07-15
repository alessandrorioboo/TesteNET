using AutoMapper;
using ITSingular.TesteNET.Data.Repository;
using ITSingular.TesteNET.DataTransfer.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using TesteNET.Application.ViewModel;

namespace TesteNET.Application
{
    public class MachineInformationAppService
    {
        private readonly MachineInformationRepository _machineInformationRepository = new MachineInformationRepository();
        private readonly MachineInformationApplicationRepository _machineInformationApplicationRepository = new MachineInformationApplicationRepository();

        public MachineInformationViewModel GetMachineInformation()
        {
            IList<MachineInformation> listMachineInformation = _machineInformationRepository.GetAll().ToList();
            var machineInformation = listMachineInformation[0];
            var machineInformationViewModel = Mapper.Map<MachineInformationViewModel>(machineInformation);
            return machineInformationViewModel;
        }

        public DashboardViewModel GetDashboardData(int limitTimeMinutes)
        {
            var actualDateTimeUTC = new DateTime(2019,07,15,15,38,34,867);
            //DateTime.UtcNow;
            //2019-07-15 15:55:10.113
            //2019-07-15 15:38:34.867

            int qttOnLineMachines = _machineInformationRepository.GetQttOnLineMachines(limitTimeMinutes, actualDateTimeUTC);
            int qttOnLineMachinesAlert = _machineInformationRepository.GetQttOnLineMachinesAlert(limitTimeMinutes, actualDateTimeUTC);
            int qttOfflineMachines = _machineInformationRepository.GetQttOfflineMachines(limitTimeMinutes, actualDateTimeUTC);
            IList<MachineInformation> top10MachinesLongerTime = _machineInformationRepository.GetTop10MachinesLongerTime();
            IList<MachineInformationApplication> top10ApplicationLeastOccurrence = _machineInformationApplicationRepository.GetTop10ApplicationLeastOccurrence();

            DashboardViewModel dashboardViewModel = new DashboardViewModel();            
            dashboardViewModel.QttOnLineMachines = qttOnLineMachines;
            dashboardViewModel.QttOnLineMachinesAlert = qttOnLineMachinesAlert;
            dashboardViewModel.QttOfflineMachines = qttOfflineMachines;
            dashboardViewModel.Top10MachinesLongerTime = Mapper.Map<IList<MachineInformationViewModel>>(top10MachinesLongerTime);
            dashboardViewModel.Top10ApplicationLeastOccurrence = Mapper.Map<IList<MachineInformationApplicationViewModel>>(top10ApplicationLeastOccurrence);
            return dashboardViewModel;
        }
    }
}
