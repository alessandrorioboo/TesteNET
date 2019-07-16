using System;

namespace TesteNET.Application.ViewModel
{
    public class UserAuthorizationViewModel
    {
        public UserAuthorizationViewModel()
        {
            IdUserAuthorization = Guid.NewGuid();
        }

        public Guid IdUserAuthorization { get; set; }
        public string Identity { get; set; }
        public bool IsAdmin { get; set; }
    }
}
