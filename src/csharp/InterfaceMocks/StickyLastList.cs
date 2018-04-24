using System.Collections.Generic;
using System.Linq;

namespace InterfaceMocks
{
    public sealed class StickyLastList<T> : IStickyLastList<T>
    {
        private readonly IList<T> _items;
        private int _actionIndex;

        public StickyLastList(params T[] items) : this(new List<T>(items)) { }

        private StickyLastList(IList<T> items) => _items = items;

        public void SetTo(IEnumerable<T> items)
        {
            _items.Clear();
            foreach (T action in items)
            {
                Add(action);
            }
        }

        public void Add(T item) => _items.Add(item);

        public T Next() => EndOfFuncs() ? _items[_items.Count - 1] : _items[_actionIndex++];
        public bool IsEmpty() => !_items.Any();

        private bool EndOfFuncs() => _items.Count == _actionIndex;
    }

    public interface IStickyLastList<T>
    {
        void SetTo(IEnumerable<T> items);
        void Add(T item);
        T Next();
        bool IsEmpty();
    }

}