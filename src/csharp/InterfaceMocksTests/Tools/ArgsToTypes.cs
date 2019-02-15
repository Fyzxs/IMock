using System;

namespace InterfaceMocksTests.Tools
{
    public sealed class ArgsToTypes
    {
        private readonly object[] _args;
        public ArgsToTypes(params object[] args) => _args = args;

        public Type[] Types()
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