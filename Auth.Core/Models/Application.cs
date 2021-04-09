﻿using System;

namespace Auth.Core.Models
{
    public class Application
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        
        public string CreatorId { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        public string WebsiteUrl { get; set; }
        public string RedirectUrl { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}