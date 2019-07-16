using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using TesteNET.Application;
using TesteNET.Application.ViewModel;

namespace TesteNET.Web
{
    public class TesteNETRolerProvider : RoleProvider
    {
        private readonly UserAuthorizationAppService _userAuthorizationAppService = new UserAuthorizationAppService();

        public override string ApplicationName {
            get => 
                throw new NotImplementedException();
            set => 
                throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            IList<string> roles = new List<string>();
            var userAuthorizationViewModel = _userAuthorizationAppService.GetByIdentity(username);
            if (userAuthorizationViewModel != null)
            {                
                if (userAuthorizationViewModel.IsAdmin)
                    roles.Add("Administrators");
            }
            else
            {
                userAuthorizationViewModel = new UserAuthorizationViewModel
                {
                    Identity = username,
                    IsAdmin = true
                };
                _userAuthorizationAppService.Add(userAuthorizationViewModel);
                roles.Add("Administrators");
            }

            return roles.ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}