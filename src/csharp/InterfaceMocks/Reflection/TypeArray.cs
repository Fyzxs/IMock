using System;

namespace InterfaceMocks.Reflection
{
    public class TypeArray : Array<Type>
    {
        private readonly object[] _args;
        public TypeArray(params object[] args) => _args = args;

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

    public abstract class Array<T>
    {
        public static implicit operator T[] (Array<T> origin) => origin.Value();
        protected abstract T[] Value();

    }
}