using FizzBuzzExample.Library.Ints;
using FizzBuzzExample.Library.Texts;

namespace FizzBuzzExample.Actions
{
    public class FizzBuzzResponseAction : IResponseAction
    {
        private readonly IResponseAction _nextAction;
        private static readonly Text FizzBuzzText = new FizzBuzzText();
        private static readonly Int Fifteen = new IntOf(15);
        public FizzBuzzResponseAction(IResponseAction nextAction) => _nextAction = nextAction;
        public Text Act(Int value)
        {
            if (value.IsEvenlyDivisibleBy(Fifteen)) return FizzBuzzText;
            return _nextAction.Act(value);
        }
    }
}