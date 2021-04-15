using System;
using System.Collections.Generic;

namespace Auth.Core.Models.Auth
{
    public class UserApplicationAccess
    {
        public string ApplicationClientId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> Scopes { get; set; }

        public ICollection<UserApplicationCodeRequest> UserApplicationCodeRequests { get; set; }
        public ICollection<UserApplicationSession> UserApplicationSessions { get; set; }
    }
}