using System;

namespace Auth.Core.Services.TokenService.Models
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}