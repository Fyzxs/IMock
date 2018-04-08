namespace Fyzxs.IMockResharperPlugin.FluentTypes.Texts {
    public sealed class CamelCaseText : Text
    {
        private readonly Text _origin;

        public CamelCaseText(Text origin) => _origin = origin;

        protected override string RawValue() => ((string)_origin).ToLower().Substring(0, 1) + ((string)_origin).Substring(1);
    }
}