using AutoMapper;
using ITSingular.TesteNET.Data.Repository;
using ITSingular.TesteNET.DataTransfer.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using TesteNET.Application.ViewModel;

namespace TesteNET.Application
{
    public class UserAuthorizationAppService
    {
        private readonly UserAuthorizationRepository _repository = new UserAuthorizationRepository();

        public void Add(UserAuthorizationViewModel userAuthorizationViewModel)
        {
            _repository.Add(Mapper.Map<UserAuthorization>(userAuthorizationViewModel));
        }

        public UserAuthorizationViewModel GetByIdentity(string identity)
        {
            UserAuthorization userAuthorization  = _repository.GetByIdentity(identity);
            return Mapper.Map<UserAuthorizationViewModel>(userAuthorization);
        }

        public IList<UserAuthorizationViewModel> GetAllOrdered()
        {
            IList<UserAuthorization> listUserAuthorization = _repository.GetAllOrdered();
            return Mapper.Map<IList<UserAuthorizationViewModel>>(listUserAuthorization);
        }

        public UserAuthorizationViewModel GetByIdAsNoTracking(Guid idUserAuthorization)
        {
            var userAuthorization = _repository.GetByIdAsNoTracking(idUserAuthorization);
            return Mapper.Map<UserAuthorizationViewModel>(userAuthorization);
        }

        public void Update(UserAuthorizationViewModel userAuthorizationViewModel)
        {
            var userAuthorization = Mapper.Map<UserAuthorization>(userAuthorizationViewModel);
            _repository.Update(userAuthorization);
        }
    }
}
