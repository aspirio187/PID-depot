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
            return _userRepository.GetById(id).MapFromDAL();
        }

        public IEnumerable<UserDto> GetUsers()
        {
            IEnumerable<UserEntity> usersFromRepo = _userRepository.GetAll();
            return usersFromRepo.Select(u => u.MapFromDAL());
        }

        public UserDto CreateUser(UserCreationDto user)
        {
            Guid createId = _userRepository.Create(user.MapToDAL());
            return _userRepository.GetById(createId).MapFromDAL();
        }

        public UserDto UserLogin(string email, string password)
        {
            return _userRepository.LogIn(email, password).MapFromDAL();
        }

        public UserDto UpdateUser(UserUpdateDto user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));

            return _userRepository.Update(user.Id, user.MapToDAL()) ? _userRepository.GetById(user.Id).MapFromDAL() : null;
        }
    }
}
