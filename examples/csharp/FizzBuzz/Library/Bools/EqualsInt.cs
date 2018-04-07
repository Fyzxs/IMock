using FizzBuzzExample.Library.Ints;

namespace FizzBuzzExample.Library.Bools
{
    public class EqualsInt : Bool
    {
        private readonly Int _leftHandSide;
        private readonly Int _rightHandSide;

        public EqualsInt(Int leftHandSide, Int rightHandSide)
        {
            _leftHandSide = leftHandSide;
            _rightHandSide = rightHandSide;
        }

        protected override bool RawValue() => (int)_leftHandSide == (int)_rightHandSide;
    }
}