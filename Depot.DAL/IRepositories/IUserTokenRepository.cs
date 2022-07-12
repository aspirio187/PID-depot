using Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depot.DAL.IRepositories
{
    public interface IUserTokenRepository : IRepositoryBase<int, UserTokenEntity>
    {
        IEnumerable<UserTokenEntity> GetUserTokens(Guid userId);
        bool TokenIsValid(Guid userId, string tokenValue);
    }
}
