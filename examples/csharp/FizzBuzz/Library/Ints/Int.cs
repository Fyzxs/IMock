using FizzBuzzExample.Library.Bools;

namespace FizzBuzzExample.Library.Ints
{
    public abstract class Int
    {
        public static implicit operator int(Int origin) => origin.RawValue();

        public Bool IsEvenlyDivisibleBy(Int divisor) => new EvenlyDivisibleBy(this, divisor);

        protected abstract int RawValue();
    }
}