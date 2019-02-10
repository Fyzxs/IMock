using Fyzxs.IFakeResharperPlugin.FluentTypes.Numbers.Dbls;
using Fyzxs.IFakeResharperPlugin.FluentTypes.Numbers.Ints;

namespace Fyzxs.IFakeResharperPlugin.FluentTypes.Numbers
{
    public abstract class Number
    {
        public abstract Int AsInt();
        public abstract Dbl AsDbl();
    }
}
