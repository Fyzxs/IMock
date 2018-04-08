using System.Linq;

namespace MicroObjectFakesResharperPlugin.FluentTypes.Texts {
    public class TextsToStringArray : Array<string>
    {
        private readonly Text[] _texts;

        public TextsToStringArray(params Text[] texts) => _texts = texts;

        protected override string[] RawValue() => _texts.Select(text => (string)text).ToArray();
    }
}