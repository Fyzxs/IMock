using FizzBuzzExample.Library.Ints;
using FizzBuzzExample.Library.Texts;

namespace FizzBuzzExample.Actions
{
    public class StrategyResponseAction : IResponseAction
    {
        private readonly IResponseAction _nextAction;

        public StrategyResponseAction() : this(
            new FizzBuzzResponseAction(
                new BuzzResponseAction(
                    new FizzResponseAction(
                        new NumberResponseAction())))
        )
        { }
        public StrategyResponseAction(IResponseAction nextAction) => _nextAction = nextAction;

        public Text Act(Int value) => _nextAction.Act(value);
    }
}