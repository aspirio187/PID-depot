using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depot.BLL.Dtos.UserTokenDtos
{
    public enum UserTokenType
    {
        EmailConfirmation
    }

    public class UserTokenDto
    {
        public int Id { get; set; }
        public UserTokenType TokenType { get; set; }
        public string Token { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Guid UserId { get; set; }
    }
}
