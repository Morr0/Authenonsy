using System;
using Auth.Core.Services.TimeService;

namespace Auth.Api.Services.TimeService
{
    public class TimeService : ITimeService
    {
        public DateTime GetDateTime()
        {
            return DateTime.UtcNow;
        }
    }
}