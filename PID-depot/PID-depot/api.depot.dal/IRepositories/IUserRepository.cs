using Api.Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.DAL.IRepositories
{
    public interface IUserRepository : IRepositoryBase<Guid, UserEntity>
    {
        UserEntity LogIn(string email, string password);
        bool AddRole(Guid userId, Guid roleId);
    }
}
