using System.Numerics;
using System.Text;

namespace PiSpigot
{
    internal static class Utility
    {
        const int AsciiOffsetOfZero = 48;

        public static string Collate<T>(this IEnumerable<T> values, int upperLimit, string separator = "")
        {
            if (typeof(T) == typeof(string))
                return values.Collate(upperLimit, separator);

            return values.ToStrings().Collate(upperLimit, separator);
        }

        public static string Collate(this IEnumerable<string> strings, int N, string separator = "")
        {
            StringBuilder sb = new StringBuilder();
            var n = strings.Take(N);
            foreach (string s in n)
            {
                sb.Append(s);
                sb.Append(separator);
            }
            return sb.ToString();
        }

        public static IEnumerable<string> ToStrings<T>(this IEnumerable<T> values)
        {
            if (values == null)
                return Enumerable.Empty<string>();

            return values.Select(v => v?.ToString() ?? "");
        }

        public static int? ToDigit(this int input)
        {
            var digit = input - AsciiOffsetOfZero;
            if (digit >= 0 && digit <= 9)
                return digit;

            return null;
        }

        public static IEnumerable<int> ToDigits(this string input)
        {
            foreach (int c in input)
            {
                var digit = c.ToDigit();
                if (digit != null)
                    yield return digit.Value;
            }
        }

        public static (bool, T, IEnumerable<T>) Pop<T>(this IEnumerable<T> sequence) where T : struct
        {
            if (sequence == null || !sequence.Any())
                return (false, default, Enumerable.Empty<T>());

            return (true, sequence.First(), sequence.Skip(1));
        }
    }
}