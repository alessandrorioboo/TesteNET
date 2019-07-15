using AutoMapper;
using ITSingular.TesteNET.DataTransfer.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteNET.Application.ViewModel;

namespace TesteNET.Application
{
    public class AutoMapperConfig
    {
        public static void RegisterMapping()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<MachineInformation, MachineInformationViewModel>();
                cfg.CreateMap<MachineInformationViewModel, MachineInformation>();

                cfg.CreateMap<MachineInformationAppService, MachineInformationApplicationViewModel>();
                cfg.CreateMap<MachineInformationApplicationViewModel, MachineInformationAppService>();
            });
        }
    }
}
