using ITSingular.TesteNET.DataTransfer.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;

namespace ITSingular.TesteNET.Data.Repository
{
    public class MachineInformationApplicationRepository : BaseRepository<MachineInformationApplication>
    {
        public void AddListMachineInformationApplication(List<MachineInformationApplication> listMachineInformationApplication)
        {
            this.AddWithTransaction(listMachineInformationApplication);
        }

        public void RemoveByMachineInformationId(Guid idMachineInformation)
        {
            Db.MachineInformationApplication.Where(c => c.MachineInformationId == idMachineInformation).Delete();
        }
    }
}
