using System.Collections.Generic;
using System.Linq;

namespace InterfaceMocks
{
    /// <inheritdoc/>
    public sealed class StickyLastList<T> : IStickyLastList<T>
    {
        private readonly IList<T> _items;
        private int _actionIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="StickyLastList{T}"/> class.
        /// </summary>
        /// <param name="items">A set of items to initialize the collection</param>
        public StickyLastList(params T[] items) : this(new List<T>(items)) { }

        private StickyLastList(IList<T> items) => _items = items;

        /// <inheritdoc/>
        public void SetTo(IEnumerable<T> items)
        {
            _items.Clear();
            foreach (T action in items)
            {
                Add(action);
            }
        }

        /// <inheritdoc/>
        public void Add(T item) => _items.Add(item);

        /// <inheritdoc/>
        public T Next() => EndOfFuncs() ? _items[_items.Count - 1] : _items[_actionIndex++];

        /// <inheritdoc/>
        public bool IsEmpty() => !_items.Any();

        private bool EndOfFuncs() => _items.Count == _actionIndex;
    }

    /// <summary>
    /// A collection that will continue to return the last item once the end is reached.
    /// </summary>
    /// <typeparam name="T">The type the collection holds</typeparam>
    public interface IStickyLastList<T>
    {
        /// <summary>
        /// Sets the collection to the provided objects.
        /// </summary>
        /// <param name="items">The collection to replace the existing element(s).</param>
        void SetTo(IEnumerable<T> items);

        /// <summary>
        /// Add a new item to the collection.
        /// </summary>
        /// <param name="item">The item to be added to the collection.</param>
        void Add(T item);

        /// <summary>
        /// Return the next item in the collection
        /// </summary>
        /// <returns>The next item in the collection</returns>
        T Next();

        /// <summary>
        /// Checks if the collection is empty.
        /// </summary>
        /// <returns>True if no items in the collection</returns>
        bool IsEmpty();
    }

}