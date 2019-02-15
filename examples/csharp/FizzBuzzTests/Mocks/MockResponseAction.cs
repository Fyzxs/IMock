using FizzBuzzExample.Actions;
using FizzBuzzExample.Library.Ints;
using FizzBuzzExample.Library.Texts;
using InterfaceMocks;

namespace FizzBuzzExampleTests.Mocks {
    public class MockResponseAction : IResponseAction
    {
        public class Builder
        {
            private readonly MockMethodWithParamAndResponse<Int, Text> _actItem = new MockMethodWithParamAndResponse<Int, Text>("MockResponseAction#Act");

            public Builder Act(params Text[] responseValue)
            {
                _actItem.UpdateInvocation(responseValue);
                return this;
            }

            public MockResponseAction Build()
            {
                return new MockResponseAction
                {
                    _act = _actItem
                };
            }
        }

        private MockMethodWithParamAndResponse<Int, Text> _act;
        private MockResponseAction() { }
        public Text Act(Int paramValue) => _act.Invoke(paramValue);
    }
}