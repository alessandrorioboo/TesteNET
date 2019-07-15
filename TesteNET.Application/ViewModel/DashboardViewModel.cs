using ITSingular.TesteNET.DataTransfer.Entityes;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TesteNET.Application.ViewModel
{
    public class DashboardViewModel
    {
        //Quantidade de máquinas online(que se comunicaram dentro do espaço de tempo limite)
        [DisplayName("ONLINE machines")]
        public int QttOnLineMachines { get; set; }
        //Quantidade de máquinas em alerta(que se comunicaram dentro do espaço de tempo limite * 1.5)
        [DisplayName("ONLINE ALERT machines")]
        public int QttOnLineMachinesAlert { get; set; }
        //Quantidade de máquinas offline(que se comunicaram a mais do que o espaço de tempo limite * 1.5)
        [DisplayName("OFFLINE machines")]
        public int QttOfflineMachines { get; set; }
        //Tabela com as top 10 máquinas que não se comunicam com o servidor a mais tempo
        public IList<MachineInformationViewModel> Top10MachinesLongerTime { get; set; }
        //Tabela com as top 10 aplicações com menos ocorrências em máquinas
        public IList<MachineInformationApplicationViewModel> Top10ApplicationLeastOccurrence { get; set; }
        [DisplayName("Min")]
        public int? LimitTimeMinutes { get; set; }
    }
}
