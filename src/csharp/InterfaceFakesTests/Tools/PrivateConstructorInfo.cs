using System;
using System.Reflection;

namespace InterfaceFakesTests.Tools
{
    public class PrivateConstructorInfo
    {
        private readonly Type _type;
        private readonly ArgsToTypes _args;
        public PrivateConstructorInfo(Type type, object[] args) : this(type, new ArgsToTypes(args)) { }

        private PrivateConstructorInfo(Type type, ArgsToTypes args)
        {
            _type = type;
            _args = args;
        }

        public ConstructorInfo CtorInfo() => _type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, _args.Types(), null);
    }
}