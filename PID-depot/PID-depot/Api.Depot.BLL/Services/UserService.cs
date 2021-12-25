﻿using Api.Depot.BLL.Dtos;
using Api.Depot.BLL.IServices;
using Api.Depot.DAL.Entities;
using Api.Depot.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));
        }

        public bool DeleteUser(Guid id)
        {
            return _userRepository.Delete(id);
        }

        public UserDto GetUser(Guid id)
        {
            UserEntity userFromRepo = _userRepository.GetById(id);
            return new UserDto(userFromRepo);
        }

        public IEnumerable<UserDto> GetUsers()
        {
            IEnumerable<UserEntity> usersFromRepo = _userRepository.GetAll();
            return usersFromRepo.Select(u => new UserDto(u));
        }
    }
}
