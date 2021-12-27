namespace Api.Depot.UIL.Models
{
    public class JwtModel
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpirationInDays { get; set; }
        public string Secret { get; set; }
    }
}
