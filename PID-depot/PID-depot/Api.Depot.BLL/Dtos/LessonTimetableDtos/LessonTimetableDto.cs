﻿using Api.Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL.Dtos.LessonTimetableDtos
{
    public class LessonTimetableDto
    {
        public int Id { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
        public int LessonId { get; set; }

        public LessonTimetableDto()
        {

        }

        public LessonTimetableDto(LessonTimetableEntity lessonTimetable)
        {
            Id = lessonTimetable.Id;
            StartsAt = lessonTimetable.StartsAt;
            EndsAt = lessonTimetable.EndsAt;
            LessonId = lessonTimetable.LessonId;
        }

        public LessonTimetableEntity MapDAL()
        {
            return new LessonTimetableEntity()
            {
                Id = Id,
                StartsAt = StartsAt,
                EndsAt = EndsAt,
                LessonId = LessonId,
            };
        }
    }
}