namespace PiSpigot
{
    internal class BaseChange : Spigot<int>
    {
        public readonly int Base1;
        public readonly int Base2;
        public readonly IEnumerable<int> Input;
        public BaseChange(int base1, int base2, IEnumerable<int> input)
        {
            Base1 = base1;
            Base2 = base2;
            Input = input;
        }
        protected override IEnumerable<int> GetSequence() => BaseChangeStream(Base1, Base2, Input);

        public static IEnumerable<int> BaseChangeStream(int m, int n, IEnumerable<int> sequence)
        {
            var (mprime, nprime) = (m, n);
            var next = ((R,R) uv) =>
            {
                var (u, v) = uv;
                return R.Floor(u * v * nprime);
            };
            var safe = ((R,R) uv, int y) =>
            {
                var (u, v) = uv;
                return (y == R.Floor((u + 1) * v * nprime));
            };
            var prod = ((R,R) uv, int y) =>
            {
                var (u, v) = uv;
                return (u - (y / (v * nprime)), v * nprime);
            };
            var cons = ((R, R) uv, int x) =>
            {
                var (u, v) = uv;
                return (x + (u * mprime), v / mprime);
            };

            (R,R) init = (0, 1);
            return Gibbons.stream(next, safe, prod, cons, init, sequence);
        }
    }
}
