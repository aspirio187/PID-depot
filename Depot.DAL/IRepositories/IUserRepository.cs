using Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Depot.DAL.IRepositories
{
    public interface IUserRepository : IRepositoryBase<Guid, UserEntity>
    {
        UserEntity GetUser(int lessonId, Guid roleId);
        IEnumerable<UserEntity> GetUsers(int lessonId, Guid roleId);
        UserEntity LogIn(string email, string password);
        bool AddRole(Guid userId, Guid roleId);
        bool EmailExist(string email);
        bool ActivateAccount(Guid userId);
        bool AccountIsActive(Guid userid);
        bool UpdatePassword(Guid userId, string oldPassword, string newPassword);
    }
}