using ITSingular.TesteNET.DataTransfer.Entityes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ITSingular.TesteNET.Data.Repository
{
    public class MachineInformationRepository : BaseRepository<MachineInformation>
    {
        public void AddMachineInformation(MachineInformation machineInformation)
        {
            this.Add(machineInformation);
        }

        public Guid? GetIdByMachineName(string machineName)
        {
            var _result = (from machineInformation in Db.MachineInformation.AsNoTracking()
                           where machineInformation.MachineName == machineName
                           select machineInformation.IdMachineInformation).FirstOrDefault();
            return _result;
        }
    }
}
