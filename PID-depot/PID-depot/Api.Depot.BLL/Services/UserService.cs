using Api.Depot.BLL.Dtos.UserDtos;
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
            return userFromRepo is not null ? new UserDto(userFromRepo) : null;
        }

        public IEnumerable<UserDto> GetUsers()
        {
            IEnumerable<UserEntity> usersFromRepo = _userRepository.GetAll();
            return usersFromRepo.Select(u => new UserDto(u)).ToList();
        }

        public UserDto CreateUser(UserCreationDto user)
        {
            Guid createId = _userRepository.Create(user.MapDAL());
            return new UserDto(_userRepository.GetById(createId));
        }
    }
}
