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
            if (data is null) throw new ArgumentNullException(nameof(data));

            Command command = new Command("spCreateUserToken", true);
            command.AddParameter("user_id", data.UserId);
            command.AddParameter("token_type", data.TokenType);

            return int.Parse(_connection.ExecuteScalar(command).ToString());
        }

        public bool Delete(int key)
        {
            Command command = new Command("spDeleteUserToken", true);
            command.AddParameter("id", key);

            return _connection.ExecuteNonQuery(command) > 0;
        }

        public IEnumerable<UserTokenEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserTokenEntity GetById(int key)
        {
            Command command = new Command("spGetUserToken", true);
            command.AddParameter("id", key);

            return _connection.ExecuteReader(command, ut => ut.MapUserToken()).FirstOrDefault();
        }

        public bool Update(int key, UserTokenEntity data)
        {
            throw new NotImplementedException();
        }
    }
}
