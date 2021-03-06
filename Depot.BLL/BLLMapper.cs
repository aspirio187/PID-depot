using Depot.BLL.Dtos.LessonDetailDtos;
using Depot.BLL.Dtos.LessonDtos;
using Depot.BLL.Dtos.LessonFileDtos;
using Depot.BLL.Dtos.LessonTimetableDtos;
using Depot.BLL.Dtos.RoleDtos;
using Depot.BLL.Dtos.UserDtos;
using Depot.BLL.Dtos.UserLessonDtos;
using Depot.BLL.Dtos.UserTokenDtos;
using Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depot.BLL
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
                    ConcurrencyStamp = user.ConcurrencyStamp,
                    IsActivated = user.IsActivated,
                    SecurityStamp = user.SecurityStamp,
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

        #region UserLesson mapping
        public static UserLessonDto MapFromDAL(this UserLessonEntity userLesson)
        {
            return userLesson is null
                ? null
                : new UserLessonDto()
                {
                    Id = userLesson.Id,
                    LessonId = userLesson.LessonId,
                    RoleId = userLesson.RoleId,
                    UserId = userLesson.UserId,
                };
        }

        public static UserLessonEntity MapToDAL(this UserLessonCreationDto userLesson)
        {
            return userLesson is null
                ? null
                : new UserLessonEntity()
                {
                    LessonId = userLesson.LessonId,
                    RoleId = userLesson.RoleId,
                    UserId = userLesson.UserId,
                };
        }
        #endregion

        #region UserToken mapping
        public static UserTokenDto MapFromDAL(this UserTokenEntity userToken)
        {
            return userToken is null
                ? null
                : new UserTokenDto()
                {
                    Id = userToken.Id,
                    TokenType = (UserTokenType)Enum.Parse(typeof(UserTokenType), userToken.TokenType),
                    Token = userToken.TokenValue,
                    DeliveryDate = userToken.DeliveryDate,
                    ExpirationDate = userToken.ExpirationDate,
                    UserId = userToken.UserId,
                };
        }

        public static UserTokenEntity MapToDAL(this UserTokenCreationDto userTokenCreation)
        {
            return userTokenCreation is null
                ? null
                : new UserTokenEntity()
                {
                    TokenType = userTokenCreation.TokenType.ToString(),
                    UserId = userTokenCreation.UserId,
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

        #region LessonTimetable mapping
        public static LessonTimetableDto MapFromDAL(this LessonTimetableEntity lessonTimetable)
        {
            return lessonTimetable is null
                ? null
                : new LessonTimetableDto()
                {
                    Id = lessonTimetable.Id,
                    StartsAt = lessonTimetable.StartsAt,
                    EndsAt = lessonTimetable.EndsAt,
                    LessonId = lessonTimetable.LessonId,
                };
        }

        public static LessonTimetableEntity MapToDAL(this LessonTimetableDto lessonTimetable)
        {
            return lessonTimetable is null
                ? null
                : new LessonTimetableEntity()
                {
                    Id = lessonTimetable.Id,
                    StartsAt = lessonTimetable.StartsAt,
                    EndsAt = lessonTimetable.EndsAt,
                    LessonId = lessonTimetable.LessonId
                };
        }

        public static LessonTimetableEntity MapToDAL(this LessonTimetableCreationDto lessonTimetable)
        {
            return lessonTimetable is null
                ? null
                : new LessonTimetableEntity()
                {
                    StartsAt = lessonTimetable.StartsAt,
                    EndsAt = lessonTimetable.EndsAt,
                    LessonId = lessonTimetable.LessonId
                };
        }
        #endregion

        #region LessonDetail mapping
        public static LessonDetailDto MapFromDAL(this LessonDetailEntity lessonDetail)
        {
            return lessonDetail is null
                ? null
                : new LessonDetailDto()
                {
                    Id = lessonDetail.Id,
                    Details = lessonDetail.Details,
                    LessonTimetableId = lessonDetail.LessonTimetableId,
                    Title = lessonDetail.Title,
                };
        }

        public static LessonDetailEntity MapToDAL(this LessonDetailDto lessonDetail)
        {
            return lessonDetail is null
                ? null
                : new LessonDetailEntity()
                {
                    Id = lessonDetail.Id,
                    Title = lessonDetail.Title,
                    Details = lessonDetail.Details,
                    LessonTimetableId = lessonDetail.LessonTimetableId,
                };
        }

        public static LessonDetailEntity MapToDAL(this LessonDetailCreationDto lessonDetail)
        {
            return lessonDetail is null
                ? null
                : new LessonDetailEntity()
                {
                    Details = lessonDetail.Details,
                    LessonTimetableId = lessonDetail.LessonTimetableId,
                    Title = lessonDetail.Title
                };
        }
        #endregion

        #region LessonFile mapping
        public static LessonFileDto MapFromDAL(this LessonFileEntity lessonFile)
        {
            return lessonFile is null
                ? null
                : new LessonFileDto()
                {
                    Id = lessonFile.Id,
                    FilePath = lessonFile.FilePath,
                    LessonDetailId = lessonFile.LessonDetailId,
                };
        }

        public static LessonFileEntity MapToDAL(this LessonFileDto lessonFile)
        {
            return lessonFile is null
                ? null
                : new LessonFileEntity()
                {
                    Id = lessonFile.Id,
                    FilePath = lessonFile.FilePath,
                    LessonDetailId = lessonFile.LessonDetailId,
                };
        }

        public static LessonFileEntity MapToDAL(this LessonFileCreationDto lessonFile)
        {
            return lessonFile is null
                ? null
                : new LessonFileEntity()
                {
                    FilePath = lessonFile.FilePath,
                    LessonDetailId = lessonFile.LessonDetailId
                };
        }
        #endregion
    }
}
