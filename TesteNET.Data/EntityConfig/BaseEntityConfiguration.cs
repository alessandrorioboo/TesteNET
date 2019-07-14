using ITSingular.TesteNET.DataTransfer.Entityes;
using System.Data.Entity.ModelConfiguration;

namespace ITSingular.TesteNET.Data.EntityConfig
{
    public abstract class BaseConfiguration<T> : EntityTypeConfiguration<T> where T : BaseEntity
    {
    }
}
