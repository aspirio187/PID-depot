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
    public class RoleRepository : IRoleRepository
    {
        private readonly Connection _connection;

        public RoleRepository(Connection connection)
        {
            _connection = connection ??
                throw new ArgumentNullException(nameof(connection));
        }

        public Guid Create(RoleEntity data)
        {
            if (data is null) throw new ArgumentNullException(nameof(data));

            Command command = new Command("spCreateRole", true);
            command.AddParameter("name", data.Name);

            return Guid.Parse(_connection.ExecuteScalar(command).ToString());
        }

        public bool Delete(Guid key)
        {
            Command command = new Command("spDeleteRole", true);
            command.AddParameter("role_id", key);

            return _connection.ExecuteNonQuery(command) > 0;
        }

        public IEnumerable<RoleEntity> GetAll()
        {
            Command command = new Command("spGetRoles", true);

            return _connection.ExecuteReader(command, r => r.MapRole());
        }

        public RoleEntity GetById(Guid key)
        {
            Command command = new Command("spGetRole", true);
            command.AddParameter("id", key);

            return _connection.ExecuteReader(command, r => r.MapRole()).FirstOrDefault();
        }

        public RoleEntity GetRole(string roleName)
        {
            string query = "SELECT * FROM roles WHERE roles.name = @role_name";
            Command command = new Command(query);
            command.AddParameter("role_name", roleName);

            return _connection.ExecuteReader(command, r => r.MapRole()).FirstOrDefault();
        }

        public IEnumerable<RoleEntity> GetUserRoles(Guid userId)
        {
            string query = "SELECT roles.id, roles.name FROM roles INNER JOIN users_roles ON users_roles.role_id = roles.id WHERE users_roles.user_id = @user_id";
            Command command = new Command(query);
            command.AddParameter("user_id", userId.ToString());

            return _connection.ExecuteReader(command, r => r.MapRole());
        }

        public bool Update(Guid key, RoleEntity data)
        {
            if (data is null) throw new ArgumentNullException(nameof(data));

            Command command = new Command("spUpdateRole", true);
            command.AddParameter("role_id", key);
            command.AddParameter("name", data.Name);

            return _connection.ExecuteNonQuery(command) > 0;
        }
    }
}
