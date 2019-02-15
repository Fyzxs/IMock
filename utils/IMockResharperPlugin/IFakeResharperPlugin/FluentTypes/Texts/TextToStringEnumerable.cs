using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Fyzxs.IMockResharperPlugin.FluentTypes.Texts
{
    public class TextToStringEnumerable : IEnumerable<string>
    {
        private readonly Text[] _texts;

        public TextToStringEnumerable(params Text[] texts) => _texts = texts;

        public IEnumerator<string> GetEnumerator()
        {
            return _texts.Select(text => (string)text).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}