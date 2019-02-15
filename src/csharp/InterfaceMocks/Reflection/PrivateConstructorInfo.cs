using InterfaceMocks.Library;
using System;
using System.Reflection;

namespace InterfaceMocks.Reflection
{
    /// <summary>
    /// <see cref="PrivateConstructorInfo"/> gets the <see cref="ConstructorInfo"/> for the private constructor matching the arguments.
    /// </summary>
    internal sealed class PrivateConstructorInfo
    {
        private readonly Type _type;
        private readonly Type[] _types;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateConstructorInfo"/> class.
        /// </summary>
        /// <param name="type">The type of to instantiate</param>
        /// <param name="args">The arguments matching the private constructor to invoke.c</param>
        public PrivateConstructorInfo(Type type, object[] args) : this(type, new ValueTypeArray(args)) { }

        private PrivateConstructorInfo(Type type, Type[] types)
        {
            _type = type;
            _types = types;
        }

        /// <summary>
        /// Conversion to <see cref="ConstructorInfo"/>.
        /// </summary>
        /// <param name="origin"></param>
        public static implicit operator ConstructorInfo(PrivateConstructorInfo origin) => origin.CtorInfo();

        private ConstructorInfo CtorInfo() => _type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, _types, null);
    }
}