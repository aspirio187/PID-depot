﻿using Api.Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.DAL
{
    public static class DALMapper
    {
        public static UserEntity MapUser(this IDataRecord data)
        {
            return new UserEntity()
            {
                Id = (Guid)data["id"],
                Email = (string)data["email"],
                NormalizedEmail = (string)data["normalized_email"],
                Password = (string)data["password"],
                Firstname = (string)data["firstname"],
                Lastname = (string)data["lastname"],
                Birthdate = (DateTime)data["birth_date"],
                RegistrationNumber = (string)data["registration_number"],
            };
        }

        public static RoleEntity MapRole(this IDataRecord data)
        {
            return new RoleEntity()
            {
                Id = (Guid)data["id"],
                Name = (string)data["name"],
            };
        }

        public static UserRoleEntity MapUserRole(this IDataRecord data)
        {
            return new UserRoleEntity()
            {
                UserId = (Guid)data["user_id"],
                RoleId = (Guid)data["role_id"],
            };
        }

        public static LessonEntity MapLesson(this IDataRecord data)
        {
            return new LessonEntity()
            {
                Id = (int)data["id"],
                Name = (string)data["name"],
                Description = (string)data["description"],
            };
        }

        public static UserLessonEntity MapUserLesson(this IDataRecord data)
        {
            return new UserLessonEntity()
            {
                Id = (int)data["id"],
                UserId = (Guid)data["user_id"],
                LessonId = (int)data["lesson_id"]
            };
        }

        public static LessonTimetableEntity MapLessonTimetable(this IDataRecord data)
        {
            return new LessonTimetableEntity()
            {
                Id = (int)data["id"],
                StartsAt = (DateTime)data["starts_at"],
                EndsAt = (DateTime)data["ends_at"],
                LessonId = (int)data["lesson_id"],
            };
        }

        public static LessonDetailEntity MapLessonDetail(this IDataRecord data)
        {
            return new LessonDetailEntity()
            {
                Id = (int)data["id"],
                Title = (string)data["title"],
                Details = (string)data["details"],
                LessonTimetableId = (int)data["lesson_timetable_id"],
            };
        }

        public static LessonFileEntity MapLessonFile(this IDataRecord data)
        {
            return new LessonFileEntity()
            {
                Id = (int)data["id"],
                FilePath = (string)data["file_path"],
                LessonDetailId = (int)data["lesson_detail_id"]
            };
        }
    }
}