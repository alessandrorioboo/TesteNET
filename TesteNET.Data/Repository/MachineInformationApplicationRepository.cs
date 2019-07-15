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

        public IList<MachineInformationApplication> GetTop10ApplicationLeastOccurrence()
        {
            IList<MachineInformationApplication> result = new List<MachineInformationApplication>();
            var query = (from p in Db.MachineInformationApplication
                          group p by p.Application into g
                          orderby g.Count()
                          select new
                          {
                              Application = g.Key,
                              QttOccours = g.Count()
                          }).Take(10).ToList();

            query.ForEach(p => result.Add(new
                MachineInformationApplication
            {
                Application = p.Application,
                QttOccours = p.QttOccours
            }));

            //var result = (from p in Db.MachineInformationApplication
            //              orderby p.Application descending
            //             select p).Take(10).ToList();

            return result.OrderBy(p => p.QttOccours).ThenBy(p => p.Application).ToList();
        }
    }
}
