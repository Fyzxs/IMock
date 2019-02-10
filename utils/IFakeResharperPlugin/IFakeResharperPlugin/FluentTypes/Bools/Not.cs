namespace Fyzxs.IFakeResharperPlugin.FluentTypes.Bools
{
    public sealed class Not : Bool
    {
        private readonly Bool _origin;

        public Not(Bool origin) => _origin = origin;

        protected override bool RawValue() => !_origin;
    }
}