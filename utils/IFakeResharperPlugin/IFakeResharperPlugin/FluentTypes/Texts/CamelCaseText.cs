namespace Fyzxs.IFakeResharperPlugin.FluentTypes.Texts
{
    public sealed class CamelCaseText : Text
    {
        private readonly Text _origin;

        public CamelCaseText(Text origin) => _origin = origin;

        protected override string RawValue() => ((string)_origin).ToLower().Substring(0, 1) + ((string)_origin).Substring(1);
    }
    public sealed class UppercaseFirstText : Text
    {
        private readonly Text _origin;

        public UppercaseFirstText(Text origin) => _origin = origin;

        protected override string RawValue() => ((string)_origin).ToUpper().Substring(0, 1) + ((string)_origin).Substring(1);
    }
}