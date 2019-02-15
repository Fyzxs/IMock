namespace InterfaceMocks.Library
{
    internal abstract class Array<T>
    {
        /// <summary>
        /// Implicit conversion from the object to an array of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="origin"></param>
        public static implicit operator T[] (Array<T> origin) => origin.Value();

        /// <summary>
        /// Overriding class provides an array of type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>An array of type <typeparamref name="T"/></returns>
        protected abstract T[] Value();
    }
}