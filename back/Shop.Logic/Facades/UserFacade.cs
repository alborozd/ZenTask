using Shop.Common;
using Shop.Common.Contracts;
using Shop.Common.Services.Contracts;
using Shop.Contracts.Dal;
using Shop.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ValidationException = Shop.Logic.Exceptions.ValidationException;

namespace Shop.Logic.Facades
{
    public class UserFacade
    {
        private readonly IUsersRepository usersRepository;
        private readonly ILockService lockService;

        public UserFacade(IUsersRepository usersRepository, ILockService lockService)
        {
            this.usersRepository = usersRepository;
            this.lockService = lockService;
        }

        private void ValidateAndThrowIfErrors(User user)
        {
            User existing = usersRepository.GetByName(user.Name);
            if (existing != null)
                throw new ValidationException("Dublicated user name");
        }

        public void Create(User user)
        {
            lockService.Execute(() =>
            {
                user.Assert(() => new ArgumentNullException("user"))
                    .Do(u => ValidateAndThrowIfErrors(u))
                    .Do(u => usersRepository.Add(u));
            });
        }
    }
}
