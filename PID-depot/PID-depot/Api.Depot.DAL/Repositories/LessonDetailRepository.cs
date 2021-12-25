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
            Command command = new Command("spGetLessonDetails", true);

            return _connection.ExecuteReader(command, ld => new LessonDetailEntity(ld));
        }

        public LessonDetailEntity GetById(int key)
        {
            string query = "SELECT * FROM lesson_details WHERE id = @id";
            Command command = new Command(query);
            command.AddParameter("id", key);

            return _connection.ExecuteReader(command, ld => new LessonDetailEntity(ld)).FirstOrDefault();
        }

        public bool Update(int key, LessonDetailEntity data)
        {
            Command command = new Command("spUpdateLessonDetail", true);
            command.AddParameter("detail_id", key);
            command.AddParameter("title", data.Title);
            command.AddParameter("details", data.Details);

            return _connection.ExecuteNonQuery(command) > 0;
        }
    }
}
