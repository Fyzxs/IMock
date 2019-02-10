namespace InterfaceFakesTests.Tools
{
    public sealed class ReflectionObject<T>
    {
        private readonly PrivateConstructorInfo _privateConstructorInfo;
        private readonly object[] _args;
        public ReflectionObject(params object[] args) : this(args, new PrivateConstructorInfo(typeof(T), args)) { }

        public ReflectionObject(object[] args, PrivateConstructorInfo privateConstructorInfo)
        {
            _args = args;
            _privateConstructorInfo = privateConstructorInfo;
        }

        public T Object() => (T)_privateConstructorInfo.CtorInfo().Invoke(_args);
    }
}