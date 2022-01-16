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
    public class LessonTimetableRepository : ILessonTimetableRepository
    {
        private readonly Connection _connection;

        public LessonTimetableRepository(Connection connection)
        {
            _connection = connection ??
                throw new ArgumentNullException(nameof(connection));
        }

        public int Create(LessonTimetableEntity data)
        {
            if (data is null) throw new ArgumentNullException(nameof(data));

            Command command = new Command("spCreateLessonTimetable", true);
            command.AddParameter("lesson_id", data.LessonId);
            command.AddParameter("starts_at", data.StartsAt);
            command.AddParameter("ends_at", data.EndsAt);

            return int.Parse(_connection.ExecuteScalar(command).ToString());
        }

        public bool Delete(int key)
        {
            Command command = new Command("spDeleteLessonTimetable", true);
            command.AddParameter("timetable_id", key);

            return _connection.ExecuteNonQuery(command) > 0;
        }

        public IEnumerable<LessonTimetableEntity> GetAll()
        {
            string query = "SELECT * FROM lesson_timetables";
            Command command = new Command(query);

            return _connection.ExecuteReader(command, lt => lt.MapLessonTimetable());
        }

        public LessonTimetableEntity GetById(int key)
        {
            string query = "SELECT * FROM lesson_timetables WHERE id = @id";
            Command command = new Command(query);
            command.AddParameter("id", key);

            return _connection.ExecuteReader(command, lt => lt.MapLessonTimetable()).FirstOrDefault();
        }

        public IEnumerable<LessonTimetableEntity> GetLessonTimetables(int lessonId)
        {
            Command command = new Command("spGetLessonTimetables", true);
            command.AddParameter("lesson_id", lessonId);

            return _connection.ExecuteReader(command, lt => lt.MapLessonTimetable());
        }

        public bool Update(int key, LessonTimetableEntity data)
        {
            if (data is null) throw new ArgumentNullException(nameof(data));

            Command command = new Command("spUpdateLessonTimetable", true);
            command.AddParameter("timetable_id", key);
            command.AddParameter("starts_at", data.StartsAt);
            command.AddParameter("ends_at", data.EndsAt);

            return _connection.ExecuteNonQuery(command) > 0;
        }
    }
}
