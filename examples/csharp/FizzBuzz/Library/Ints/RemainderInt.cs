namespace FizzBuzzExample.Library.Ints
{
    public class RemainderInt : Int
    {
        private readonly Int _dividend;
        private readonly Int _divisor;

        public RemainderInt(Int dividend, Int divisor)
        {
            _dividend = dividend;
            _divisor = divisor;
        }

        protected override int RawValue() => (int)_dividend % (int)_divisor;
    }
}