using Api.Depot.BLL.Dtos.UserDtos;
using Api.Depot.UIL.Models;
using Api.Depot.UIL.Models.Forms;

namespace Api.Depot.UIL
{
    public static class UILMapper
    {
        #region User mapping
        public static UserModel MapFromBLL(this UserDto user)
        {
            return user is null
                ? null
                : new UserModel()
                {
                    Lastname = user.Lastname,
                    Birthdate = user.Birthdate,
                    Email = user.Email,
                    Firstname = user.Firstname,
                    Id = user.Id,
                    RegistrationNumber = user.RegistrationNumber,
                };
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
        #endregion
    }
}
