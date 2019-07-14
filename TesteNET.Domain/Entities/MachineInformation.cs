using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITSingular.TesteNET.DataTransfer.Entityes
{
    public class MachineInformation : BaseEntity
    {
        public MachineInformation()
        {
            IdMachineInformation = Guid.NewGuid();
        }

        public Guid IdMachineInformation { get; set; }
        public string MachineName { get; set; }
        public DateTime DateTimeUTC { get; set; }

        [NotMapped]
        public IList<String> AppListInMachineClient { get; set; }
    }
}

