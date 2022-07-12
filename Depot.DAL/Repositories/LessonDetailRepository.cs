using Depot.DAL.Entities;
using Depot.DAL.IRepositories;
using Depot.DAL.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Depot.DAL.Repositories
{
    public class LessonDetailRepository : ILessonDetailRepository
    {
        private readonly Connection _connection;

        public LessonDetailRepository(Connection connection)
        {
            _connection = connection ??
                throw new ArgumentNullException(nameof(connection));
        }

        public int Create(LessonDetailEntity data)
        {
            if (data is null) throw new ArgumentNullException(nameof(data));

            Command command = new Command("spCreateLessonDetail", true);
            command.AddParameter("title", data.Title);
            command.AddParameter("details", data.Details);
            command.AddParameter("timetable_id", data.LessonTimetableId);

            return int.Parse(_connection.ExecuteScalar(command).ToString());
        }

        public bool Delete(int key)
        {
            Command command = new Command("spDeleteLessonDetail", true);
            command.AddParameter("detail_id", key);

            return _connection.ExecuteNonQuery(command) > 0;
        }

        public IEnumerable<LessonDetailEntity> GetAll()
        {
            string query = "SELECT * FROM lesson_details";
            Command command = new Command(query);

            return _connection.ExecuteReader(command, ld => ld.MapLessonDetail());
        }

        public LessonDetailEntity GetById(int key)
        {
            string query = "SELECT * FROM lesson_details WHERE id = @id";
            Command command = new Command(query);
            command.AddParameter("id", key);

            return _connection.ExecuteReader(command, ld => ld.MapLessonDetail()).FirstOrDefault();
        }

        public IEnumerable<LessonDetailEntity> GetLessonDetails(int lessonId)
        {
            Command command = new Command("spGetLessonDetails", true);
            command.AddParameter("lesson_id", lessonId);

            return _connection.ExecuteReader(command, ld => ld.MapLessonDetail());
        }

        public IEnumerable<LessonDetailEntity> GetLessonTimetableDetails(int lessonTimetableId)
        {
            Command command = new Command("spGetLessonDetail", true);
            command.AddParameter("timetable_id", lessonTimetableId);

            return _connection.ExecuteReader(command, ld => ld.MapLessonDetail());
        }

        public bool Update(int key, LessonDetailEntity data)
        {
            if (data is null) throw new ArgumentNullException(nameof(data));

            Command command = new Command("spUpdateLessonDetail", true);
            command.AddParameter("detail_id", key);
            command.AddParameter("title", data.Title);
            command.AddParameter("details", data.Details);

            return _connection.ExecuteNonQuery(command) > 0;
        }
    }
}
