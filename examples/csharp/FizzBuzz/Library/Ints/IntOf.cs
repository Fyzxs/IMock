﻿using System;

namespace FizzBuzzExample.Library.Ints
{
    public class IntOf : Int
    {
        private readonly Func<int> _origin;
        public IntOf(int origin) : this(() => origin) { }
        private IntOf(Func<int> origin) => _origin = origin;

        protected override int RawValue() => _origin();
    }
}