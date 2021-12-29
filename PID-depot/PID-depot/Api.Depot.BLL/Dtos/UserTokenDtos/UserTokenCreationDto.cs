using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL.Dtos.UserTokenDtos
{
    public class UserTokenCreationDto
    {
        public UserTokenType TokenType { get; set; }
        public Guid UserId { get; set; }
    }
}
