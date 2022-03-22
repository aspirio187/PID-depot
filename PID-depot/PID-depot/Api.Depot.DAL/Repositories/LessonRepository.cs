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
    public class LessonRepository : ILessonRepository
    {
        private readonly Connection _connection;

        public LessonRepository(Connection connection)
        {
            _connection = connection ??
                throw new ArgumentNullException(nameof(connection));
        }

        public bool AddLessonUser(int lessonId, Guid userId)
        {
            if (lessonId <= 0) throw new ArgumentOutOfRangeException(nameof(lessonId));
            if (userId == Guid.Empty) throw new ArgumentException(nameof(userId));

            string query = "INSERT INTO users_lessons (`user_id`, `lesson_id`) VALUES (@user_id, @lesson_id)";
            Command command = new Command(query);
            command.AddParameter("user_id", userId);
            command.AddParameter("lesson_id", lessonId);

            return _connection.ExecuteNonQuery(command) > 0;
        }

        public int Create(LessonEntity data)
        {
            if (data is null) throw new ArgumentNullException(nameof(data));

            Command command = new Command("spCreateLesson", true);
            command.AddParameter("name", data.Name);
            command.AddParameter("description", data.Description);

            return int.Parse(_connection.ExecuteScalar(command).ToString());
        }

        public bool Delete(int key)
        {
            Command command = new Command("spDeleteLesson", true);
            command.AddParameter("lesson_id", key);

            return _connection.ExecuteNonQuery(command) > 0;
        }

        public IEnumerable<LessonEntity> GetAll()
        {
            Command command = new Command("spGetLessons", true);

            return _connection.ExecuteReader(command, l => l.MapLesson());
        }

        public LessonEntity GetById(int key)
        {
            Command command = new Command("spGetLesson", true);
            command.AddParameter("id", key);

            return _connection.ExecuteReader(command, l => l.MapLesson()).FirstOrDefault();
        }

        public IEnumerable<LessonEntity> GetUserLessons(Guid userId)
        {
            string query = @"SELECT lessons.id, lessons.name, lessons.description 
                            FROM users_lessons 
                            LEFT JOIN users ON users.id = users_lessons.user_id 
                            LEFT JOIN lessons ON lessons.id = users_lessons.lesson_id
                            WHERE users.id = @user_id";

            Command command = new Command(query);
            command.AddParameter("user_id", userId);

            var lessons = _connection.ExecuteReader(command, l => l.MapLesson());
            return lessons;
        }

        public bool Update(int key, LessonEntity data)
        {
            if (data is null) throw new ArgumentNullException(nameof(data));

            Command command = new Command("spUpdateLesson", true);
            command.AddParameter("lesson_id", key);
            command.AddParameter("name", data.Name);
            command.AddParameter("description", data.Description);

            return _connection.ExecuteNonQuery(command) > 0;
        }
    }
}
