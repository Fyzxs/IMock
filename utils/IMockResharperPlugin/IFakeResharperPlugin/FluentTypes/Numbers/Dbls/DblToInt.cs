﻿using Fyzxs.IMockResharperPlugin.FluentTypes.Numbers.Ints;

namespace Fyzxs.IMockResharperPlugin.FluentTypes.Numbers.Dbls
{
    public sealed class DblToInt : Int
    {
        private readonly Dbl _origin;

        public DblToInt(Dbl origin)
        {
            _origin = origin;
        }

        protected override int RawValue() => (int)(double)_origin;
    }
}