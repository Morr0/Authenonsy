using System;
using System.Security.Cryptography;
using System.Text;
using Auth.Core.Services.TimeService;

namespace Auth.Core.Services.RandomStringService
{
    public class RandomStringService : IRandomStringService, IDisposable
    {
        private readonly ITimeService _timeService;
        private readonly SHA1 _sha1 = new SHA1CryptoServiceProvider();

        public RandomStringService(ITimeService timeService)
        {
            _timeService = timeService;
        }

        public void Dispose()
        {
            _sha1.Dispose();
        }

        public string NextValue()
        {
            var proposedStringBytes = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
            var hashBytes = _sha1.ComputeHash(proposedStringBytes);
            return Convert.ToHexString(hashBytes);
        }
    }
}