using System.Numerics;

namespace PiSpigot
{
    /// <summary>
    /// Rational numbers implemented as BigInteger pair
    /// </summary>
    internal class R
    {
        public BigInteger N => Tuple.Item1; // Numerator
        public BigInteger D => Tuple.Item2; // Denominator
        private (BigInteger, BigInteger) Tuple;
        public R(BigInteger num, BigInteger den)
            : this((num, den))
        { }
        public R((BigInteger, BigInteger) val)
        {
            Tuple = Reduce(val);
        }
        public R(BigInteger n)
            : this(n, 1)
        { }

        /// <summary>
        /// This cast to int is unsafe for arbitrary BigIntegers, but in the algorithms it is used in here, it should only end up being single digits
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static int Floor(R r) => (int)(r.N / r.D);

        public static BigInteger GreatestCommonDivisor(BigInteger b1, BigInteger b2)
        {
            if (b1 < 0) b1 = -b1;
            if (b2 < 0) b2 = -b2;
            if (b1.IsOne || b2.IsOne) return BigInteger.One;
            return BigInteger.GreatestCommonDivisor(b1, b2);
        }

        public static (BigInteger, BigInteger) Reduce((BigInteger, BigInteger) input)
        {
            var (N, D) = input;
            if (N.IsZero) return input;
            var gcd = GreatestCommonDivisor(N, D);
            if (!gcd.IsOne)
            {
                return (N / gcd, D / gcd);
            }
            return input;
        }

        public static implicit operator R((BigInteger, BigInteger) tuple) => new R(tuple);
        public static implicit operator R(BigInteger n) => new R(n);
        public static implicit operator R(int n) => new R(n);
        //public static implicit operator (BigInteger, BigInteger)(R r) => (r.Numer, r.Denom);
        public static R operator -(R r) => new R(-r.N, r.D);
        public static R operator *(R r1, R r2) => new R(r1.N * r2.N, r1.D * r2.D);
        public static R operator /(R r1, R r2) => r1 * new R(r2.D, r2.N);
        public static R operator /(BigInteger i, R r) => new R(i) / r;
        public static R operator /(R r, BigInteger i) => r / new R(i);
        public static R operator +(R r1, R r2) => new R(r1.N * r2.D + r2.N * r1.D, r1.D * r2.D);
        public static R operator -(R r1, R r2) => r1 + -r2;
        public static R operator +(R r, BigInteger i) => r + new R(i);
        public static R operator +(BigInteger i, R r) => r + i;
        public static R operator *(R r, BigInteger i) => r * new R(i);
        public override string ToString() => $"{N}/{D}";
    }
}
