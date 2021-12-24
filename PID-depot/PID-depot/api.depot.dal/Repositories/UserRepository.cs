using api.depot.dal.Entities;
using api.depot.dal.IRepositories;
using DevHopTools.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.depot.dal.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Connection _connection;

        public UserRepository(Connection connection)
        {
            _connection = connection ??
                throw new ArgumentNullException(nameof(connection));
        }

        public Guid Create(UserEntity data)
        {
            Command command = new Command("spCreateUser", true);
            command.AddParameter("email", data.Email);
            command.AddParameter("password", data.Password);
            command.AddParameter("firstname", data.Firstname);
            command.AddParameter("lastname", data.Lastname);
            command.AddParameter("birth_date", data.Birthdate.ToString("yyyy-MM-dd"));
            command.AddParameter("registration_number", data.RegistrationNumber);
            
            return Guid.Parse(_connection.ExecuteScalar(command).ToString());
        }

        public bool Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserEntity GetById(Guid key)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid key, UserEntity data)
        {
            throw new NotImplementedException();
        }
    }
}
