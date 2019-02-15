using System;

namespace InterfaceMocks.Library
{
    /// <summary>
    /// Transforms a collection of object into an array of the instance <see cref="Type"/>.
    /// </summary>
    internal sealed class ValueTypeArray : Array<Type>
    {
        private readonly object[] _args;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTypeArray"/> class.
        /// </summary>
        /// <param name="args">The Objects</param>
        public ValueTypeArray(params object[] args) => _args = args;

        /// <inheritdoc/>
        protected override Type[] Value()
        {
            Type[] types = new Type[_args.Length];
            for (int index = 0; index < _args.Length; index++)
            {
                types[index] = _args[index].GetType();
            }
            return types;
        }
    }
}