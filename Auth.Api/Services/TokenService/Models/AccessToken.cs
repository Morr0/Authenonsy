﻿using System;

namespace Auth.Api.Services.TokenService.Models
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}