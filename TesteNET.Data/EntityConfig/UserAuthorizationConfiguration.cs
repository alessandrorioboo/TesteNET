using ITSingular.TesteNET.DataTransfer.Entityes;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

namespace ITSingular.TesteNET.Data.EntityConfig
{
    public class UserAuthorizationConfiguration : BaseConfiguration<UserAuthorization>
    {
        public UserAuthorizationConfiguration()
        {
            HasKey(m => m.IdUserAuthorization);
            Property(m => m.Identity).IsRequired().HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_UserAuthorization", 1) { IsUnique = true }));
            Property(m => m.IsAdmin).IsRequired();
        }
    }
}
