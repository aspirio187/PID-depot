using Api.Depot.BLL.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL.IServices
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetUsers();
        UserDto GetUser(Guid id);
        bool DeleteUser(Guid id);
        UserDto CreateUser(UserCreationDto user);
        
    }
}
