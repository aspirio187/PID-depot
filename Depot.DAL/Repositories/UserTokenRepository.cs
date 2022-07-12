using Depot.DAL.Entities;
using Depot.DAL.IRepositories;
using Depot.DAL.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depot.DAL.Repositories
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

        public IEnumerable<UserTokenEntity> GetUserTokens(Guid userId)
        {
            Command command = new Command("spGetUserTokens", true);
            command.AddParameter("user_id", userId);

            return _connection.ExecuteReader(command, ut => ut.MapUserToken());
        }

        public bool TokenIsValid(Guid userId, string tokenValue)
        {
            Command command = new Command("spTokenIsValid", true);
            command.AddParameter("user_id", userId);
            command.AddParameter("token", tokenValue);

            return Convert.ToBoolean((long)_connection.ExecuteScalar(command));
        }

        public bool Update(int key, UserTokenEntity data)
        {
            throw new NotImplementedException();
        }
    }
}
