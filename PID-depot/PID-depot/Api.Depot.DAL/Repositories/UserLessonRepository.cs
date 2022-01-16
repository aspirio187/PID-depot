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
    public class UserLessonRepository : IUserLessonRepository
    {
        private readonly Connection _connection;

        public UserLessonRepository(Connection connection)
        {
            _connection = connection ??
                throw new ArgumentNullException(nameof(connection));
        }

        public int Create(UserLessonEntity data)
        {
            if (data is null) throw new ArgumentNullException(nameof(data));

            Command command = new Command("spCreateUserLesson", true);
            command.AddParameter("user_id", data.UserId);
            command.AddParameter("role_id", data.RoleId);
            command.AddParameter("lesson_id", data.LessonId);

            return int.Parse(_connection.ExecuteScalar(command).ToString());
        }

        public bool Delete(int key)
        {
            string query = "DELETE FROM users_lessons WHERE users_lessons.id = @id";
            Command command = new Command(query);
            command.AddParameter("id", key);

            return _connection.ExecuteNonQuery(command) > 0;
        }

        public IEnumerable<UserLessonEntity> GetAll()
        {
            string query = "SELECT * FROM users_lessons";
            Command command = new Command(query);

            return _connection.ExecuteReader(command, ul => ul.MapUserLesson());
        }

        public UserLessonEntity GetById(int key)
        {
            string query = "SELECT * FROM users_lessons WHERE users_lessons.id = @id";
            Command command = new Command(query);
            command.AddParameter("id", key);

            return _connection.ExecuteReader(command, ul => ul.MapUserLesson())
                .FirstOrDefault();
        }

        public UserLessonEntity GetByLessonKey(int id)
        {
            string query = "SELECT * FROM users_lessons WHERE users_lessons.lesson_id = @lesson_id";
            Command command = new Command(query);
            command.AddParameter("lesson_id", id);

            return _connection.ExecuteReader(command, ul => ul.MapUserLesson())
                .FirstOrDefault();
        }

        public UserLessonEntity GetByRoleKey(Guid id)
        {
            string query = "SELECT * FROM users_lessons WHERE users_lessons.role_id = @role_id";
            Command command = new Command(query);
            command.AddParameter("role_id", id);

            return _connection.ExecuteReader(command, ul => ul.MapUserLesson())
                .FirstOrDefault();
        }

        public UserLessonEntity GetByUserKey(Guid id)
        {
            string query = "SELECT * FROM users_lessons WHERE users_lessons.user_id = @user_id";
            Command command = new Command(query);
            command.AddParameter("user_id", id);

            return _connection.ExecuteReader(command, ul => ul.MapUserLesson())
                .FirstOrDefault();
        }

        public bool Update(int key, UserLessonEntity data)
        {
            throw new NotImplementedException();
        }
    }
}
