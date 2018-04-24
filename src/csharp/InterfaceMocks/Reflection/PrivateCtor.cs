using System.Reflection;

namespace InterfaceMocks.Reflection
{
    public class PrivateCtor<T>
    {
        private readonly PrivateConstructorInfo _privateConstructorInfo;
        private readonly object[] _args;
        public PrivateCtor(params object[] args) : this(args, new PrivateConstructorInfo(typeof(T), args)) { }

        public PrivateCtor(object[] args, PrivateConstructorInfo privateConstructorInfo)
        {
            _args = args;
            _privateConstructorInfo = privateConstructorInfo;
        }

        public static implicit operator T(PrivateCtor<T> origin) => origin.Object();

        private T Object() => (T)((ConstructorInfo)_privateConstructorInfo).Invoke(_args);
    }
}