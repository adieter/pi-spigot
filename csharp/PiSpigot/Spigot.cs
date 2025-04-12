using System.Collections;

namespace PiSpigot
{
    internal abstract class Spigot<T> : IEnumerable<T>
    {
        protected abstract IEnumerable<T> GetSequence();
        protected int DebugCounter;
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetSequence().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetSequence().GetEnumerator();
    }
}