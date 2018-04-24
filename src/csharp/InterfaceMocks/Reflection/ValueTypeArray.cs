using System;

namespace InterfaceMocks.Reflection
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

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal abstract class Array<T>
    {
        /// <summary>
        /// Implicit conversion from the object to an array of type <see cref="T"/>.
        /// </summary>
        /// <param name="origin"></param>
        public static implicit operator T[] (Array<T> origin) => origin.Value();

        /// <summary>
        /// Overriding class provides an array of type <see cref="T"/>.
        /// </summary>
        /// <returns>An array of type <see cref="T"/></returns>
        protected abstract T[] Value();

    }
}