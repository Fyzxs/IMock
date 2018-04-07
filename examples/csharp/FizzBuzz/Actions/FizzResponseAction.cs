using FizzBuzzExample.Library.Ints;
using FizzBuzzExample.Library.Texts;

namespace FizzBuzzExample.Actions
{
    public class FizzResponseAction : IResponseAction
    {
        private readonly IResponseAction _nextAction;
        private static readonly Text FizzText = new FizzText();
        private static readonly Int Three = new IntOf(3);
        public FizzResponseAction(IResponseAction nextAction) => _nextAction = nextAction;
        public Text Act(Int value)
        {
            if (value.IsEvenlyDivisibleBy(Three)) return FizzText;
            return _nextAction.Act(value);
        }
    }
}