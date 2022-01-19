using Api.Depot.DAL.Entities;
using Api.Depot.DAL.IRepositories;
using Api.Depot.DAL.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Api.Depot.DAL.Repositories
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
            if (data is null) throw new ArgumentNullException(nameof(data));

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
            Command command = new Command("spDeleteUser", true);
            command.AddParameter("user_id", key);
            return _connection.ExecuteNonQuery(command) > 0;
        }

        public IEnumerable<UserEntity> GetAll()
        {
            Command command = new Command("spGetUsers", true);
            return _connection.ExecuteReader(command, r => r.MapUser());
        }

        public UserEntity GetById(Guid key)
        {
            Command command = new Command("spGetUser", true);
            command.AddParameter("user_id", key);
            return _connection.ExecuteReader(command, r => r.MapUser()).FirstOrDefault();
        }

        public UserEntity LogIn(string email, string password)
        {
            Command command = new Command("spUserLogin", true);
            command.AddParameter("auth_email", email);
            command.AddParameter("auth_password", password);

            return _connection.ExecuteReader(command, u => u.MapUser()).FirstOrDefault();
        }

        public bool Update(Guid key, UserEntity data)
        {
            if (data is null) throw new ArgumentNullException(nameof(data));

            Command command = new Command("spUpdateUser", true);
            command.AddParameter("user_id", key);
            command.AddParameter("firstname", data.Firstname);
            command.AddParameter("lastname", data.Lastname);
            command.AddParameter("birth_date", data.Birthdate.ToString("yyyy-MM-dd"));
            return _connection.ExecuteNonQuery(command) > 0;
        }

        public bool AddRole(Guid userId, Guid roleId)
        {
            string query = "INSERT INTO users_roles (`user_id`, `role_id`) VALUES (@user_id, @role_id)";
            Command command = new Command(query);
            command.AddParameter("user_id", userId);
            command.AddParameter("role_id", roleId);

            return _connection.ExecuteNonQuery(command) > 0;
        }

        public bool EmailExist(string email)
        {
            Command command = new Command("spEmailExist", true);
            command.AddParameter("email", email);

            return (long)_connection.ExecuteScalar(command) == 1;
        }

        public bool ActivateAccount(Guid userId)
        {
            Command command = new Command("spActivateAccount", true);
            command.AddParameter("user_id", userId);

            return _connection.ExecuteNonQuery(command) > 0;
        }

        public bool AccountIsActive(Guid userid)
        {
            string query = "SELECT users.is_activated FROM users WHERE users.id = @user_id";
            Command command = new Command(query);
            command.AddParameter("user_id", userid);

            return _connection.ExecuteReader(command, u => (bool)u["is_activated"]).FirstOrDefault();
        }

        public bool UpdatePassword(Guid userId, string oldPassword, string newPassword)
        {
            Command command = new Command("spUpdateUserPassword", true);
            command.AddParameter("user_id", userId);
            command.AddParameter("old_password_confirmation", oldPassword);
            command.AddParameter("new_password", newPassword);

            return _connection.ExecuteNonQuery(command) > 0;
        }

        public UserEntity GetUser(int lessonId, Guid roleId)
        {
            Command command = new Command("spGetUserLessonRole", true);
            command.AddParameter("lesson_id", lessonId);
            command.AddParameter("role_id", roleId);

            return _connection.ExecuteReader(command, u => u.MapUser()).FirstOrDefault();
        }

        public IEnumerable<UserEntity> GetUsers(int lessonId, Guid roleId)
        {
            Command command = new Command("spGetUsersLessonRole", true);
            command.AddParameter("lesson_id", lessonId);
            command.AddParameter("role_id", roleId);

            return _connection.ExecuteReader(command, u => u.MapUser());
        }
    }
}
