using Api.Depot.BLL.Dtos.UserDtos;
using Api.Depot.BLL.IServices;
using Api.Depot.DAL.Entities;
using Api.Depot.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));
            _roleRepository = roleRepository ??
                throw new ArgumentNullException(nameof(roleRepository));
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

        public bool AddUserRole(Guid userId, Guid roleId)
        {
            UserEntity userFromRepo = _userRepository.GetById(userId);
            if (userFromRepo is null) return false;
            RoleEntity roleFromRepo = _roleRepository.GetById(roleId);
            if (roleFromRepo is null) return false;

            return _userRepository.AddRole(userId, roleId);
        }

        public bool UserExist(Guid userId)
        {
            return _userRepository.GetById(userId) is not null;
        }

        public bool EmailExist(string email)
        {
            if (email is null) throw new ArgumentNullException(nameof(email));
            if (email.Length == 0) throw new ArgumentException($"{nameof(email)} can not be empty string!");
            return _userRepository.EmailExist(email);
        }

        public bool ActivateAccount(Guid userId)
        {
            return _userRepository.ActivateAccount(userId);
        }

        public bool AccountIsActive(Guid userId)
        {
            return _userRepository.AccountIsActive(userId);
        }

        public bool UpdatePassword(Guid userId, string oldPassword, string newPassword)
        {
            if (userId == Guid.Empty) throw new Exception($"{nameof(userId)} cannot be empty!");
            if (oldPassword is null) throw new ArgumentNullException(nameof(oldPassword));
            if (newPassword is null) throw new ArgumentNullException(nameof(newPassword));

            return _userRepository.UpdatePassword(userId, oldPassword, newPassword);
        }

        public UserDto GetUserLesson(int lessonId, Guid roleId)
        {
            if (lessonId <= 0) throw new ArgumentOutOfRangeException(nameof(lessonId));
            if (roleId == Guid.Empty) throw new ArgumentNullException(nameof(roleId));

            UserEntity userFromRepo = _userRepository.GetUser(lessonId, roleId);
            if (userFromRepo is null) return null;

            return userFromRepo.MapFromDAL();
        }
    }
}
