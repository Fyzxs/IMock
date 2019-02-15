using System.Collections.Generic;

namespace Fyzxs.IMockResharperPlugin.FluentTypes.Texts
{
    public sealed class JoinText : Text
    {
        private readonly Text _separator;
        private readonly IEnumerable<string> _stringEnumerable;

        public JoinText(Text separator, params Text[] texts) : this(separator, new TextToStringEnumerable(texts))
        {
        }

        public JoinText(Text separator, IEnumerable<string> stringEnumerable)
        {
            _separator = separator;
            _stringEnumerable = stringEnumerable;
        }

        protected override string RawValue() => string.Join(_separator, _stringEnumerable);
    }

    public sealed class SpaceJoin : Text
    {
        private readonly Text _text;
        public SpaceJoin(params Text[] texts) : this(new JoinText(new TextOf(" "), texts)) { }
        private SpaceJoin(Text text) => _text = text;

        protected override string RawValue() => _text;
    }
}