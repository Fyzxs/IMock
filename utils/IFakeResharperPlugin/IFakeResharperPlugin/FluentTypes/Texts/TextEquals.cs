using Fyzxs.IFakeResharperPlugin.FluentTypes.Bools;

namespace Fyzxs.IFakeResharperPlugin.FluentTypes.Texts
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