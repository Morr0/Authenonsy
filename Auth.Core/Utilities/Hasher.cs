namespace Auth.Core.Utilities
{
    public static class Hasher
    {
        private static int _hashingFactor = 12;
        
        public static string Hash(string text)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(text, _hashingFactor);
        }

        public static bool IsSameHash(string hash, string text)
        {
            return BCrypt.Net.BCrypt.Verify(text, hash);
        }
    }
}