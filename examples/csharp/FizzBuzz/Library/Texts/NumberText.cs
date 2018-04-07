using FizzBuzzExample.Library.Ints;

namespace FizzBuzzExample.Library.Texts
{
    public class NumberText : Text
    {
        private readonly Int _origin;

        public NumberText(Int origin) => _origin = origin;

        protected override string RawValue() => ((int)_origin).ToString();
    }
}