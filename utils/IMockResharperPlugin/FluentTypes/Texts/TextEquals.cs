using MicroObjectFakesResharperPlugin.FluentTypes.Bools;

namespace MicroObjectFakesResharperPlugin.FluentTypes.Texts
{
    public class TextEquals : Bool
    {
        private readonly Text _leftHandSide;
        private readonly Text _rightHandSide;

        public TextEquals(Text leftHandSide, Text rightHandSide)
        {
            _leftHandSide = leftHandSide;
            _rightHandSide = rightHandSide;
        }
        protected override bool RawValue() => (string)_leftHandSide == (string)_rightHandSide;
    }
}