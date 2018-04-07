using FizzBuzzExample.Library.Ints;
using FizzBuzzExample.Library.Texts;

namespace FizzBuzzExample.Actions
{
    public class BuzzResponseAction : IResponseAction
    {
        private readonly IResponseAction _nextAction;
        private static readonly Text BuzzText = new BuzzText();
        private static readonly Int Five = new IntOf(5);
        public BuzzResponseAction(IResponseAction nextAction) => _nextAction = nextAction;
        public Text Act(Int value)
        {
            if (value.IsEvenlyDivisibleBy(Five)) return BuzzText;
            return _nextAction.Act(value);
        }
    }
}


