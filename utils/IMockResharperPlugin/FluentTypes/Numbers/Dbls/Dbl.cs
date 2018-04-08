using MicroObjectFakesResharperPlugin.FluentTypes.Numbers.Ints;

namespace MicroObjectFakesResharperPlugin.FluentTypes.Numbers.Dbls
{
    public abstract class Dbl : Number
    {
        public static implicit operator double(Dbl origin) => origin.RawValue();

        protected abstract double RawValue();

        public sealed override Int AsInt() => new DblToInt(this);

        public sealed override Dbl AsDbl() => this;
    }
}