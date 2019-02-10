namespace Fyzxs.IFakeResharperPlugin.FluentTypes.Texts
{
    public abstract class Text
    {
        public static implicit operator string(Text origin) => origin.RawValue();

        public Text Format(params Text[] others) => new FormatText(this, others);
        public Text CamelCase() => new CamelCaseText(this);
        protected abstract string RawValue();
    }
}