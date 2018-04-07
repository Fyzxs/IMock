using System;

namespace FizzBuzzExample.Library.Texts
{
    public class TextOf : Text
    {
        private readonly Func<string> _origin;

        public TextOf(int origin) : this(origin.ToString) { }

        public TextOf(string origin) : this(() => origin) { }

        private TextOf(Func<string> origin) => _origin = origin;

        protected override string RawValue() => _origin();
    }
}