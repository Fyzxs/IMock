using FizzBuzzExample.Library.Ints;
using InterfaceMocks;

namespace FizzBuzzExampleTests.Mocks
{
    public class MockInt : Int
    {
        public class Builder
        {
            private readonly MockMethodWithResponse<int> _mockRawValue = new MockMethodWithResponse<int>("MockInt#RawValue");

            public Builder RawValue(params int[] returnValue)
            {
                _mockRawValue.UpdateInvocation(returnValue);
                return this;
            }

            public MockInt Build()
            {
                return new MockInt()
                {
                    _rawValue = _mockRawValue,
                };
            }
        }

        private MockMethodWithResponse<int> _rawValue;
        private MockInt() { }

        protected override int RawValue() => _rawValue.Invoke();
    }
}