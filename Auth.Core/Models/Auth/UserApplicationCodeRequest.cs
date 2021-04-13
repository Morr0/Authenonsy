using System;

namespace Auth.Core.Models.Auth
{
    public class UserApplicationCodeRequest
    {
        public string Code { get; set; }
        public UserApplicationAccess ApplicationAccess { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}