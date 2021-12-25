using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.DAL.Entities
{
    public class UserLessonEntity
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public int LessonId { get; set; }

        public UserLessonEntity()
        {

        }

        public UserLessonEntity(IDataRecord data)
        {
            Id = (int)data["id"];
            UserId = (Guid)data["user_id"];
            RoleId = (Guid)data["role_id"];
            LessonId = (int)data["lesson_id"];
        }
    }
}
