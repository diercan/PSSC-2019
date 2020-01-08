using System;
using System.Linq;

namespace FrBaschet.Infrastructure.Helpers
{
    public static class SecurityHelper
    {
        public static string GetRandomString(int length = 30)
        {
            var random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}