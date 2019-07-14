using System;
using System.Collections.Generic;

namespace ITSingular.TesteNET.DataTransfer
{
    public class MachineInformation
    {
        public string MachineName { get; set; }
        public DateTime DateTimeUTC { get; set; }
        public IList<string> AppListInMachineClient { get; set; }
    }
}
