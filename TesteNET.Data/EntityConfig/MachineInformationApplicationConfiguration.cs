using ITSingular.TesteNET.DataTransfer.Entityes;

namespace ITSingular.TesteNET.Data.EntityConfig
{
    public class MachineInformationApplicationConfiguration : BaseConfiguration<MachineInformationApplication>
    {
        public MachineInformationApplicationConfiguration()
        {
            HasKey(m => m.IdMachineInformationApplication);
            Property(m => m.Application).IsRequired().HasMaxLength(256);
            this.HasRequired(x => x.MachineInformation)
                .WithMany()
                .HasForeignKey(x => x.MachineInformationId);
        }
    }
}
