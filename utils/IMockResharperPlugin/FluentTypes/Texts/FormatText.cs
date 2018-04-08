namespace MicroObjectFakesResharperPlugin.FluentTypes.Texts {
    public class FormatText : Text
    {
        private readonly Text _format;
        private readonly Array<string> _stringEnumberable;

        public FormatText(Text format, params Text[] texts) : this(format, new TextsToStringArray(texts)) { }
        public FormatText(Text format, Array<string> stringEnumberable)
        {
            _format = format;
            _stringEnumberable = stringEnumberable;
        }

        protected override string RawValue() => string.Format(_format, _stringEnumberable);
    }
}