using System.Reflection;

namespace InterfaceMocks.Reflection
{
    /// <summary>
    /// Creates instance of a class from a private constructor.
    /// <example>
    /// Usage:
    /// <code>
    /// ClassToInstantiate newInstance = new PrivateCtor&lt;ClassToInstantiate>, "ValueForFirstCtorArg", valueForSecondCtorArg)
    /// </code>
    /// </example>
    /// </summary>
    public sealed class PrivateCtor<T>
    {
        private readonly PrivateConstructorInfo _privateConstructorInfo;
        private readonly object[] _args;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateCtor{T}"/> class.
        /// </summary>
        /// <param name="args">The arguments that will be provided to the default constructor.</param>
        public PrivateCtor(params object[] args) : this(args, new PrivateConstructorInfo(typeof(T), args)) { }

        private PrivateCtor(object[] args, PrivateConstructorInfo privateConstructorInfo)
        {
            _args = args;
            _privateConstructorInfo = privateConstructorInfo;
        }

        /// <summary>
        /// Convertor to specified type T.
        /// </summary>
        /// <param name="origin"></param>
        public static implicit operator T(PrivateCtor<T> origin) => origin.Object();

        private T Object() => (T)((ConstructorInfo)_privateConstructorInfo).Invoke(_args);
    }
}