using FizzBuzzExample.Library.Texts;

namespace FizzBuzzExample.Library.Bools
{
    public class EqualsText : Bool
    {
        private readonly Text _leftHandSide;
        private readonly Text _rightHandSide;

        public EqualsText(Text leftHandSide, Text rightHandSide)
        {
            _leftHandSide = leftHandSide;
            _rightHandSide = rightHandSide;
        }
        protected override bool RawValue() => (string)_leftHandSide == (string)_rightHandSide;
    }
}