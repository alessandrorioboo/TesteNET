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
        private readonly MachineInformationRepository _repository = new MachineInformationRepository();

        public MachineInformationViewModel GetMachineInformation()
        {
            IList<MachineInformation> listMachineInformation = _repository.GetAll().ToList();
            var machineInformation = listMachineInformation[0];
            var machineInformationViewModel = Mapper.Map<MachineInformationViewModel>(machineInformation);
            return machineInformationViewModel;
        }
    }
}
