namespace PiSpigot
{

    internal class RabinowitzWagon : Spigot<int>
    {
        private readonly int N;
        public RabinowitzWagon(int n)
        {
            N = n;
        }
        protected override IEnumerable<int> GetSequence() => Build(N).Skip(1); // skip leading zero of original algorithm

        private IEnumerable<int> Build(int n)
        {
            var len = (10 * n) / 3;
            var a = new int[len];
            for (int j = 0; j < len; j++)
            {
                a[j] = 2;
            }
            var nines = 0;
            var predigit = 0;
            var q = 0;
            for (int j = 0; j < n; j++)
            {
                for (int i = len - 1; i > 0; i--)
                {
                    var x = 10 * a[i] + q * i;
                    a[i] = x % (2 * i - 1);
                    q = x / (2 * i - 1);
                }
                a[1] = q % 10;
                q = q / 10;
                if (q == 9) { nines++; }
                else if (q == 10)
                {
                    yield return Prepare(predigit + 1);
                    for (int k = 0; k < nines; k++)
                    {
                        yield return Prepare(0);
                    }
                    predigit = 0;
                    nines = 0;
                }
                else
                {
                    yield return Prepare(predigit);
                    predigit = q;
                    if (nines != 0)
                    {
                        for (int k = 0; k < nines; k++)
                        {
                            yield return Prepare(9);
                        }
                        nines = 0;
                    }
                }
            }
            yield return Prepare(predigit);
        }

        private int Prepare(int digit)
        {
            DebugCounter++;
            return digit;
        }
    }
}