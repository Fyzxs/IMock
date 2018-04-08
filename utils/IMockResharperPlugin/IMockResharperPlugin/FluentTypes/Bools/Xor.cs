namespace Fyzxs.IMockResharperPlugin.FluentTypes.Bools
{
    public sealed class Xor : Bool
    {
        private readonly Bool _boolA;
        private readonly Bool _boolB;

        public Xor(Bool boolA, Bool boolB)
        {
            _boolA = boolA;
            _boolB = boolB;
        }

        protected override bool RawValue() => _boolA ^ _boolB;
    }
}