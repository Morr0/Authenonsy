using System;

namespace Auth.Auth.Api.Services.TokenService.Models
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }

        public string RefreshToken { get; set; }
    }
}