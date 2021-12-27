using Api.Depot.DAL.Entities;
using Api.Depot.DAL.IRepositories;
using Api.Depot.DAL.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.DAL.Repositories
{
    public class UserTokenRepository : IUserTokenRepository
    {
        private readonly Connection _connection;

        public UserTokenRepository(Connection connection)
        {
            _connection = connection ??
                throw new ArgumentNullException(nameof(connection));
        }

        public int Create(UserTokenEntity data)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserTokenEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserTokenEntity GetById(int key)
        {
            throw new NotImplementedException();
        }

        public bool Update(int key, UserTokenEntity data)
        {
            throw new NotImplementedException();
        }
    }
}
