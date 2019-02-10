using FizzBuzzExample.Library.Ints;
using InterfaceFakes;

namespace FizzBuzzExampleTests.Fakes
{
    public class FakeInt : Int
    {
        public class Builder
        {
            private readonly FakeMethodWithResponse<int> _fakeRawValue = new FakeMethodWithResponse<int>("FakeInt#RawValue");

            public Builder RawValue(params int[] returnValue)
            {
                _fakeRawValue.UpdateInvocation(returnValue);
                return this;
            }

            public FakeInt Build()
            {
                return new FakeInt()
                {
                    _rawValue = _fakeRawValue,
                };
            }
        }

        private FakeMethodWithResponse<int> _rawValue;
        private FakeInt() { }

        protected override int RawValue() => _rawValue.Invoke();
    }
}