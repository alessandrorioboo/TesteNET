using System;

namespace ITSingular.TesteNET.DataTransfer.Entityes
{
    public class MachineInformationApplication : BaseEntity
    {
        public MachineInformationApplication()
        {
            IdMachineInformationApplication = Guid.NewGuid();
        }
        public Guid IdMachineInformationApplication { get; set; }
        public string Application { get; set; }
        public Guid MachineInformationId { get; set; }
        public virtual MachineInformation MachineInformation { get; set; }

    }
}

