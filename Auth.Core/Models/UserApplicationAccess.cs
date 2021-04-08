using System;

namespace Auth.Core.Models
{
    public class UserApplicationAccess
    {
        public string ApplicationClientId { get; set; }
        public string UserId { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}