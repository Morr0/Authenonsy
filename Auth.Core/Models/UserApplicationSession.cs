using System;

namespace Auth.Core.Models
{
    public class UserApplicationSession
    {
        public string AccessToken { get; set; }
        public DateTime CreatedAt { get; set; }

        public string ApplicationId { get; set; }
    }
}