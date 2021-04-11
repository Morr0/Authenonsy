using System;

namespace Auth.Api.Services.TokenService.Models
{
    public class CodeToken
    {
        public string Code { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}