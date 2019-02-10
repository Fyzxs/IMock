using FizzBuzzExample.Actions;
using FizzBuzzExample.Library.Ints;
using FizzBuzzExample.Library.Texts;
using InterfaceFakes;

namespace FizzBuzzExampleTests.Fakes {
    public class FakeResponseAction : IResponseAction
    {
        public class Builder
        {
            private readonly FakeMethodWithParamAndResponse<Int, Text> _actItem = new FakeMethodWithParamAndResponse<Int, Text>("FakeResponseAction#Act");

            public Builder Act(params Text[] responseValue)
            {
                _actItem.UpdateInvocation(responseValue);
                return this;
            }

            public FakeResponseAction Build()
            {
                return new FakeResponseAction
                {
                    _act = _actItem
                };
            }
        }

        private FakeMethodWithParamAndResponse<Int, Text> _act;
        private FakeResponseAction() { }
        public Text Act(Int paramValue) => _act.Invoke(paramValue);
    }
}