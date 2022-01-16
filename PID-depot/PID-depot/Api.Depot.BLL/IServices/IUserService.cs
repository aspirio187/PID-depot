using Api.Depot.BLL.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL.IServices
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetUsers();
        UserDto GetUser(Guid id);
        UserDto GetUserLesson(int lessonId, Guid roleId);
        bool DeleteUser(Guid id);
        UserDto CreateUser(UserCreationDto user);
        UserDto UserLogin(string email, string password);
        UserDto UpdateUser(UserUpdateDto user);
        bool AddUserRole(Guid userId, Guid roleId);
        bool EmailExist(string email);
        bool ActivateAccount(Guid userId);
        bool AccountIsActive(Guid userId);
        bool UpdatePassword(Guid userId, string oldPassword, string newPassword);
    }
}
