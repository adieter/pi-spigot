namespace PiSpigot
{
    internal class Math
    {
        public static IEnumerable<int> Naturals(int max = 0)
        {
            for (int k = 1; ; k++)
            {
                yield return k;
                if (max > 0 && k > max) yield break;
            }
        }

        public static IEnumerable<int> Fibonacci()
        {
            var (i, j) = (1, 1);
            yield return i;
            yield return j;

            while (true)
            {
                (i, j) = (j, i + j);
                yield return j;
            }
        }
    }
}
