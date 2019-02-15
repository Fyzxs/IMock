using Fyzxs.IMockResharperPlugin.FluentTypes.Numbers.Dbls;

namespace Fyzxs.IMockResharperPlugin.FluentTypes.Numbers.Ints
{
    public abstract class Int : Number
    {
        public static implicit operator int(Int origin) => origin.RawValue();

        protected abstract int RawValue();

        public sealed override Int AsInt() => this;

        public sealed override Dbl AsDbl()
        {
            throw new System.NotImplementedException();
        }
    }
}