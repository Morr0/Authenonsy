using System;

namespace Auth.Core.Models.Auth
{
    public class UserApplicationSession
    {
        public string AccessToken { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public UserApplicationAccess ApplicationAccess { get; set; }
        public string RefreshToken { get; set; }
    }
}