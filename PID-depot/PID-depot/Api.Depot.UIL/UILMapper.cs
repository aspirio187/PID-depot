using Api.Depot.BLL.Dtos.LessonDtos;
using Api.Depot.BLL.Dtos.RoleDtos;
using Api.Depot.BLL.Dtos.UserDtos;
using Api.Depot.UIL.Models;
using Api.Depot.UIL.Models.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Api.Depot.UIL
{
    public static class UILMapper
    {
        #region User mapping
        public static UserModel MapFromBLL(this UserDto user, IEnumerable<RoleDto> roles = null)
        {
            if (user is null) return null;
            UserModel userModel = new UserModel()
            {
                Lastname = user.Lastname,
                Birthdate = user.Birthdate,
                Email = user.Email,
                Firstname = user.Firstname,
                Id = user.Id,
                RegistrationNumber = user.RegistrationNumber,
                SecurityStamp = user.SecurityStamp,
                IsActivated = user.IsActivated,
                ConcurrencyStamp = user.ConcurrencyStamp,
            };

            if (roles is null) return userModel;

            userModel.Roles = roles.Select(r => r.MapFromBLL());
            return userModel;
        }
        public static UserDto MapToBLL(this UserModel user)
        {
            return user is null
                ? null
                : new UserDto()
                {
                    Birthdate = user.Birthdate,
                    Email = user.Email,
                    Firstname = user.Firstname,
                    Id = user.Id,
                    Lastname = user.Lastname,
                    RegistrationNumber = user.RegistrationNumber,
                    ConcurrencyStamp = user.ConcurrencyStamp,
                    IsActivated = user.IsActivated,
                    SecurityStamp = user.SecurityStamp,
                };
        }

        public static UserUpdateDto MapToBLL(this UserForm user)
        {
            return user is null
                ? null
                : new UserUpdateDto()
                {
                    Id = user.Id,
                    Birthdate = user.Birthdate,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                };
        }

        public static UserCreationDto MapToBLL(this RegisterForm register)
        {
            return register is null
                ? null
                : new UserCreationDto()
                {
                    Birthdate = register.Birthdate,
                    Email = register.Email,
                    Firstname = register.Firstname,
                    Lastname = register.Lastname,
                    Password = register.Password,
                };
        }
        #endregion

        #region Role mapping
        public static RoleModel MapFromBLL(this RoleDto role)
        {
            return role is null
                ? null
                : new RoleModel()
                {
                    Id = role.Id,
                    Name = role.Name,
                };
        }

        public static RoleDto MapToBLL(this RoleModel role)
        {
            return role is null
                ? null
                : new RoleDto()
                {
                    Id = role.Id,
                    Name = role.Name
                };
        }

        public static RoleCreationDto MapToBLL(this RoleForm role)
        {
            return role is null
                ? null
                : new RoleCreationDto()
                {
                    Name = role.Name
                };
        }
        #endregion

        #region Lesson mapping
        public static LessonModel MapFromBLL(this LessonDto lesson, UserDto teacher)
        {
            if (lesson is null) return null;
            if (teacher is null) return null;

            return new LessonModel()
            {
                Description = lesson.Description,
                Id = lesson.Id,
                Name = lesson.Name,
                TeacherName = $"{teacher.Lastname.ToUpper()} {teacher.Firstname}",
                TeacherRegistrationNumber = teacher.RegistrationNumber
            };
        }

        public static LessonDto MapToBLL(this LessonModel lesson)
        {
            return lesson is null
                ? null
                : new LessonDto()
                {
                    Description = lesson.Description,
                    Id = lesson.Id,
                    Name = lesson.Name
                };
        }

        public static LessonCreationDto MapToBLL(this LessonForm lesson)
        {
            return lesson is null
                ? null
                : new LessonCreationDto()
                {
                    Description = lesson.Description,
                    Name = lesson.Name,
                };
        }
        #endregion
    }
}
