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

        public int GetQttOnLineMachines(int limitTimeMinutes, DateTime actualDateTimeUTC)
        {
            return (from machineInformation in Db.MachineInformation.AsNoTracking()
                    where DbFunctions.DiffMinutes(machineInformation.DateTimeUTC, actualDateTimeUTC) <= limitTimeMinutes
                    select machineInformation).Count();
        }

        public int GetQttOnLineMachinesAlert(int limitTimeMinutes, DateTime actualDateTimeUTC)
        {
            var limitTimeMinutesTolerance = (limitTimeMinutes * 1.5);
            return (from machineInformation in Db.MachineInformation.AsNoTracking()
                    where DbFunctions.DiffMinutes(machineInformation.DateTimeUTC, actualDateTimeUTC) > limitTimeMinutes &&
                          DbFunctions.DiffMinutes(machineInformation.DateTimeUTC, actualDateTimeUTC) <= limitTimeMinutesTolerance
                    select machineInformation).Count();
        }

        public int GetQttOfflineMachines(int limitTimeMinutes, DateTime actualDateTimeUTC)
        {
            var limitTimeMinutesTolerance = (limitTimeMinutes * 1.5);
            return (from machineInformation in Db.MachineInformation.AsNoTracking()
                    where DbFunctions.DiffMinutes(machineInformation.DateTimeUTC, actualDateTimeUTC) > limitTimeMinutesTolerance
                    select machineInformation).Count();
        }

        public IList<MachineInformation> GetTop10MachinesLongerTime()
        {
            return (from machineInformation in Db.MachineInformation.AsNoTracking()
                    orderby machineInformation.DateTimeUTC
                    select machineInformation).Take(10).ToList();
        }

    }
}
