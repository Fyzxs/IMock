using FizzBuzzExample.Library.Bools;

namespace FizzBuzzExample.Library.Texts
{
    public abstract class Text
    {
        public static implicit operator string(Text origin) => origin.RawValue();

        public Bool IsEqual(Text other) => new EqualsText(this, other);

        protected abstract string RawValue();
    }
}