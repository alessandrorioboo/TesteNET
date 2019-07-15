using System;

namespace TesteNET.Application.ViewModel
{
    public class MachineInformationViewModel
    {
        public MachineInformationViewModel()
        {
            IdMachineInformation = Guid.NewGuid();
        }

        public Guid IdMachineInformation { get; set; }
        public string MachineName { get; set; }
        public DateTime DateTimeUTC { get; set; }
    }
}
