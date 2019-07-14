using ITSingular.TesteNET.DataTransfer.Entityes;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

namespace ITSingular.TesteNET.Data.EntityConfig
{
    public class MachineInformationConfiguration : BaseConfiguration<MachineInformation>
    {
        public MachineInformationConfiguration()
        {
            HasKey(m => m.IdMachineInformation);
            Property(m => m.MachineName).IsRequired().HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_MachineInformation", 1) { IsUnique = true }));
            Property(m => m.DateTimeUTC).IsRequired();
        }
    }
}
