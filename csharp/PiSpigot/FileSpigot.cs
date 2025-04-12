using System.Collections;

namespace PiSpigot
{
    internal class FileSpigot : Spigot<int>
    {
        private readonly string Filename;
        public FileSpigot(string filename) { Filename = filename; }
        protected override IEnumerable<int> GetSequence() => ReadFile(Filename);

        private IEnumerable<int> ReadFile(string filename)
        {
            using var fs = new FileStream(filename, FileMode.Open);
            while (fs.CanRead)
            {
                var b = fs.ReadByte();
                var digit = b.ToDigit();
                if (digit != null)
                    yield return Prepare(digit.Value);
            }
            yield break;
        }
        private int Prepare(int digit)
        {
            DebugCounter++;
            return digit;
        }
    }
}
