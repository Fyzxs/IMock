using System;
using System.Reflection;

namespace InterfaceMocks.Reflection
{
    public class PrivateConstructorInfo
    {
        private readonly Type _type;
        private readonly Type[] _types;
        public PrivateConstructorInfo(Type type, object[] args) : this(type, new TypeArray(args)) { }

        private PrivateConstructorInfo(Type type, Type[] types)
        {
            _type = type;
            _types = types;
        }

        public static implicit operator ConstructorInfo(PrivateConstructorInfo origin) => origin.CtorInfo();

        private ConstructorInfo CtorInfo()
        {
            //return _type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
            return _type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, _types, null);
        }
    }
}