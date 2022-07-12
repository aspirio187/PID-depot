using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depot.BLL.Dtos.UserLessonDtos
{
    public class UserLessonCreationDto
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public int LessonId { get; set; }
    }
}
