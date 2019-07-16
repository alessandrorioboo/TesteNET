using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITSingular.TesteNET.DataTransfer.Entityes
{
    public class UserAuthorization : BaseEntity
    {
        public UserAuthorization()
        {
            IdUserAuthorization = Guid.NewGuid();
        }

        public Guid IdUserAuthorization { get; set; }
        public string Identity { get; set; }
        public bool IsAdmin { get; set; }
    }
}

