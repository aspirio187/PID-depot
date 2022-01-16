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

            return (int)_connection.ExecuteScalar(command);
        }

        public bool Delete(int key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserLessonEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserLessonEntity GetById(int key)
        {
            throw new NotImplementedException();
        }

        public bool Update(int key, UserLessonEntity data)
        {
            throw new NotImplementedException();
        }
    }
}
