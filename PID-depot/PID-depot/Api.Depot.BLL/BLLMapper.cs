using Api.Depot.BLL.Dtos.LessonDtos;
using Api.Depot.BLL.Dtos.RoleDtos;
using Api.Depot.BLL.Dtos.UserDtos;
using Api.Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL
{
    public static class BLLMapper
    {
        #region User mapping
        public static UserDto MapFromDAL(this UserEntity user)
        {
            return user is null
                ? null
                : new UserDto()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Birthdate = user.Birthdate,
                    RegistrationNumber = user.RegistrationNumber,
                };
        }

        public static UserEntity MapToDAL(this UserCreationDto user)
        {
            return user is null
                ? null
                : new UserEntity()
                {
                    Email = user.Email,
                    Password = user.Password,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Birthdate = user.Birthdate,
                    RegistrationNumber = "u" + new Random().Next(1, 1000000000).ToString("D6")
                };
        }

        public static UserEntity MapToDAL(this UserUpdateDto user)
        {
            return user is null
                ? null
                : new UserEntity()
                {
                    Id = user.Id,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Birthdate = user.Birthdate,
                };
        }
        #endregion

        #region Role mapping
        public static RoleDto MapFromDAL(this RoleEntity role)
        {
            return role is null
                ? null
                : new RoleDto()
                {
                    Id = role.Id,
                    Name = role.Name,
                };
        }

        public static RoleEntity MapToDAL(this RoleDto role)
        {
            return role is null
                ? null
                : new RoleEntity()
                {
                    Id = role.Id,
                    Name = role.Name
                };
        }

        public static RoleEntity MapToDAL(this RoleCreationDto role)
        {
            return role is null
                ? null
                : new RoleEntity()
                {
                    Name = role.Name
                };
        }
        #endregion

        #region Lesson mapping
        public static LessonDto MapFromDAL(this LessonEntity lesson)
        {
            return lesson is null
                ? null
                : new LessonDto()
                {
                    Id = lesson.Id,
                    Description = lesson.Description,
                    Name = lesson.Name,
                };
        }

        public static LessonEntity MapToDAL(this LessonDto lesson)
        {
            return lesson is null
                ? null
                : new LessonEntity()
                {
                    Id = lesson.Id,
                    Description = lesson.Description,
                    Name = lesson.Name
                };
        }

        public static LessonEntity MapToDAL(this LessonCreationDto lesson)
        {
            return lesson is null
                ? null
                : new LessonEntity()
                {
                    Description = lesson.Description,
                    Name = lesson.Name
                };
        }
        #endregion
    }
}
