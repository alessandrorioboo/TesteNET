using ITSingular.TesteNET.DataTransfer.Entityes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ITSingular.TesteNET.Data.Repository
{
    public class UserAuthorizationRepository : BaseRepository<UserAuthorization>
    {
        public void AddUserAuthorization(UserAuthorization userAuthorization)
        {
            this.Add(userAuthorization);
        }

        public UserAuthorization GetByIdentity(string identity)
        {
            return (from userAuthorization in Db.UserAuthorization.AsNoTracking()
                    where userAuthorization.Identity == identity
                    select userAuthorization).FirstOrDefault();
        }

        public IList<UserAuthorization> GetAllOrdered()
        {
            return (from userAuthorization in Db.UserAuthorization.AsNoTracking()
                    orderby userAuthorization.Identity
                    select userAuthorization).ToList();

        }

        public UserAuthorization GetByIdAsNoTracking(Guid idUserAuthorization)
        {
            return (from userAuthorization in Db.UserAuthorization.AsNoTracking()
                    where userAuthorization.IdUserAuthorization == idUserAuthorization
                    select userAuthorization).FirstOrDefault();

        }
    }
}
