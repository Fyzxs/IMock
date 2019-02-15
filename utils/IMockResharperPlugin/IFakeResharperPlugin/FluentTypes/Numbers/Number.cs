using Fyzxs.IMockResharperPlugin.FluentTypes.Numbers.Dbls;
using Fyzxs.IMockResharperPlugin.FluentTypes.Numbers.Ints;

namespace Fyzxs.IMockResharperPlugin.FluentTypes.Numbers
{
    public abstract class Number
    {
        public abstract Int AsInt();
        public abstract Dbl AsDbl();
    }
}
