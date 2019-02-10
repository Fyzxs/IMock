using System.Diagnostics;

namespace Fyzxs.IFakeResharperPlugin.FluentTypes.Bools
{

    [DebuggerDisplay("{RawValue()}")]
    public abstract partial class Bool
    {
        public static readonly Bool True = new BoolOf(true);
        public static readonly Bool False = new BoolOf(false);

        public static implicit operator bool(Bool origin) => origin.RawValue();

        protected abstract bool RawValue();
    }
}