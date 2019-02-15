using System.Threading;

namespace InterfaceMocks.Library
{
    /// <inheritdoc/>
    internal class Counter : ICounter
    {
        private long _count;

        /// <inheritdoc/>
        public long Value() => Interlocked.Read(ref _count);

        /// <inheritdoc/>
        public void Increment() => Interlocked.Increment(ref _count);
    }

    /// <summary>
    /// A class to count. This is thread safe through use of the <see cref="Interlocked"/>.
    /// </summary>
    internal interface ICounter
    {
        /// <summary>
        /// Current value.
        /// </summary>
        /// <returns>The current value of the counter</returns>
        long Value();

        /// <summary>
        /// Increment the counter by 1.
        /// </summary>
        void Increment();
    }
}