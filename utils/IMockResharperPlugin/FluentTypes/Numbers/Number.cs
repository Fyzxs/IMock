using MicroObjectFakesResharperPlugin.FluentTypes.Numbers.Dbls;
using MicroObjectFakesResharperPlugin.FluentTypes.Numbers.Ints;

namespace MicroObjectFakesResharperPlugin.FluentTypes.Numbers
{
    public abstract class Number
    {
        public abstract Int AsInt();
        public abstract Dbl AsDbl();
    }
}
