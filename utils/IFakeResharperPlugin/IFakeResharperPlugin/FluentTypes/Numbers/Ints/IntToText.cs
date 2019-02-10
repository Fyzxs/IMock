using Fyzxs.IFakeResharperPlugin.FluentTypes.Texts;

namespace Fyzxs.IFakeResharperPlugin.FluentTypes.Numbers.Ints
{
    public class IntToText : Text
    {
        private readonly Int _origin;

        public IntToText(Int origin) => _origin = origin;

        protected override string RawValue() => ((int)_origin).ToString();
    }
}