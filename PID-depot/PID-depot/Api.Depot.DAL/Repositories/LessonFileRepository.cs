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
    public class LessonFileRepository : ILessonFileRepository
    {
        private readonly Connection _connection;

        public LessonFileRepository(Connection connection)
        {
            _connection = connection ??
                throw new ArgumentNullException(nameof(connection));
        }

        public int Create(LessonFileEntity data)
        {
            if (data is null) throw new ArgumentNullException(nameof(data));

            Command command = new Command("spCreateLessonFile", true);
            command.AddParameter("filepath", data.FilePath);
            command.AddParameter("lesson_detail_id", data.LessonDetailId);

            return int.Parse(_connection.ExecuteScalar(command).ToString());
        }

        public bool Delete(int key)
        {
            Command command = new Command("spDeleteLessonFile", true);
            command.AddParameter("file_id", key);

            return _connection.ExecuteNonQuery(command) > 0;
        }

        public IEnumerable<LessonFileEntity> GetAll()
        {
            string query = "SELECT * FROM lesson_files";
            Command command = new Command(query);

            return _connection.ExecuteReader(command, lf => lf.MapLessonFile());
        }

        public LessonFileEntity GetById(int key)
        {
            string query = "SELECT * FROM lesson_files WHERE id = @id";
            Command command = new Command(query);

            return _connection.ExecuteReader(command, lf => lf.MapLessonFile()).FirstOrDefault();
        }

        public IEnumerable<LessonFileEntity> GetLessonDetailFiles(int lessonDetailId)
        {
            Command command = new Command("spGetLessonDetailFiles", true);
            command.AddParameter("details_id", lessonDetailId);

            return _connection.ExecuteReader(command, lf => lf.MapLessonFile());
        }

        public IEnumerable<LessonFileEntity> GetLessonFiles(int lessonId)
        {
            Command command = new Command("spGetLessonFiles", true);
            command.AddParameter("lesson_id", lessonId);

            return _connection.ExecuteReader(command, lf => lf.MapLessonFile());
        }

        public bool Update(int key, LessonFileEntity data)
        {
            if (data is null) throw new ArgumentNullException(nameof(data));

            Command command = new Command("spUpdateLessonFile", true);
            command.AddParameter("file_id", key);
            command.AddParameter("filepath", data.FilePath);

            return _connection.ExecuteNonQuery(command) > 0;
        }
    }
}
