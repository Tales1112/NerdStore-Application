using System;

namespace NSE.Identidade.API.Model
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public Guid Token { get; set; }
        public DateTime ExpirationDate { get; set; }

        public RefreshToken()
        {
            Id = Guid.NewGuid();
            Token = Guid.NewGuid();
        }
    }
}
