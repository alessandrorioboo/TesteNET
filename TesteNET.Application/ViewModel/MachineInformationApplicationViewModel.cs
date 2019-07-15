using System;

namespace TesteNET.Application.ViewModel
{
    public class MachineInformationApplicationViewModel
    {
        public MachineInformationApplicationViewModel()
        {
            IdMachineInformationApplication = Guid.NewGuid();
        }

        public Guid IdMachineInformationApplication { get; set; }
        public string Application { get; set; }
        public Guid MachineInformationId { get; set; }
        public virtual MachineInformationViewModel MachineInformation { get; set; }

        public int QttOccours { get; set; }
    }
}
