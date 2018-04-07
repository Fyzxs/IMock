using FizzBuzzExample.Library.Ints;

namespace FizzBuzzExample.Library.Bools
{
    public class EvenlyDivisibleBy : Bool
    {
        private static readonly Int Zero = new IntOf(0);

        private readonly Bool _origin;

        public EvenlyDivisibleBy(Int dividend, Int divisor) : this(new EqualsInt(new RemainderInt(dividend, divisor), Zero)) { }

        public EvenlyDivisibleBy(Bool origin) => _origin = origin;

        protected override bool RawValue() => _origin;
    }
}