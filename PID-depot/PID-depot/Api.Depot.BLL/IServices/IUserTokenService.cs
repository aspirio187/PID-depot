﻿using Api.Depot.BLL.Dtos.UserTokenDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL.IServices
{
    public interface IUserTokenService
    {
        bool TokenIsValid(Guid userId, string token);
        UserTokenDto CreateUserToken(UserTokenCreationDto userToken);
    }
}
