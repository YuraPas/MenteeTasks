using System;
using System.Linq;

namespace XboxStatistics.Extensions
{
    public static class ByteArrayExtensions
    {
        public static string ToHex(this byte[] a)
        {
            return string.Join(string.Empty, a.Select(b => b.ToString("X2")));
        }

        public static void SwapEndian(this byte[] a, int div)
        {
            var temp = new byte[div];
            for (var i = 0; i < a.Length; i += div)
            {
                Buffer.BlockCopy(a, i, temp, 0, div);
                Array.Reverse(temp);
                Buffer.BlockCopy(temp, 0, a, i, div);
            }
        }
    }
}