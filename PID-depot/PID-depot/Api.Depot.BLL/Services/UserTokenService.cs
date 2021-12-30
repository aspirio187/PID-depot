using Api.Depot.BLL.Dtos.UserTokenDtos;
using Api.Depot.BLL.IServices;
using Api.Depot.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL.Services
{
    public class UserTokenService : IUserTokenService
    {
        private readonly IUserTokenRepository _userTokenRepository;

        public UserTokenService(IUserTokenRepository userTokenRepository)
        {
            _userTokenRepository = userTokenRepository ??
                throw new ArgumentNullException(nameof(userTokenRepository));
        }

        public UserTokenDto CreateUserToken(UserTokenCreationDto userToken)
        {
            if (userToken is null) throw new ArgumentNullException(nameof(userToken));

            int userTokenCreatedId = _userTokenRepository.Create(userToken.MapToDAL());
            return _userTokenRepository.GetById(userTokenCreatedId).MapFromDAL();
        }

        public IEnumerable<UserTokenDto> GetUserTokens(Guid userId)
        {
            return _userTokenRepository.GetUserTokens(userId).Select(ut => ut.MapFromDAL());
        }

        public bool TokenIsValid(Guid userId, string token)
        {
            return _userTokenRepository.TokenIsValid(userId, token);
        }
    }
}
