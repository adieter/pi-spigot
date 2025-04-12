using System.Numerics;

namespace PiSpigot
{
    internal class Gibbons : Spigot<int>
    {
        protected override IEnumerable<int> GetSequence() => pi1();
        public IEnumerable<int> pi1()
        {
            var comp = (LFT lft1, LFT lft2) =>
            {
                var (q, r, s, t) = lft1;
                var (u, v, w, x) = lft2;
                var result = (q * u + r * w, q * v + r * x, s * u + t * w, s * v + t * x);
                return new LFT(result);
            };

            var extr = (LFT lft, int x) =>
            {
                var (q, r, s, t) = lft;
                var result = (q * x + r, s *x + t);
                return new R(result);
            };

            var lfts = Math.Naturals().Select(k => new LFT((k, 4 * k + 2, 0, 2 * k + 1)));
            var next = (LFT z) => R.Floor(extr(z, 3));
            var safe = (LFT z, int n)
                => (n == R.Floor(extr(z, 4)));
            var prod = (LFT z, int n)
                => comp((10, -10 * n, 0, 1), z);
            var cons = (LFT z, LFT zprime)
                => comp(z, zprime);

            LFT init = (1, 0, 0, 1);
            return stream(next, safe, prod, cons, init, lfts);

        }

        public static IEnumerable<C> stream<A, B, C>(
            Func<B, C> next,  
            Func<B, C, bool> safe,    
            Func<B, C, B> prod,    
            Func<B, A, B> cons,    
            B z,    
            IEnumerable<A> xxs
            ) where A : struct where C : struct
        {
            while (true)
            {
                var y = next(z);
                if (safe(z, y))
                {
                    yield return y;
                    z = prod(z, y);
                }
                else
                {
                    (bool isValid, A x, xxs) = xxs.Pop();
                    if (!isValid)
                    {
                        yield break;
                    }
                    z = cons(z, x);
                }
            }
        }
    }

    public struct LFT
    {
        public readonly (BigInteger, BigInteger, BigInteger, BigInteger) Tuple;
        public LFT((BigInteger, BigInteger, BigInteger, BigInteger) tuple)
        {
            Tuple = tuple;
        }
        public static implicit operator LFT((BigInteger, BigInteger, BigInteger, BigInteger) tuple)
        {
            return new LFT(tuple);
        }
        public override string ToString() => Tuple.ToString();

        internal void Deconstruct(out BigInteger q, out BigInteger r, out BigInteger s, out BigInteger t)
        {
            (q, r, s, t) = Tuple;
        }
    }
}
