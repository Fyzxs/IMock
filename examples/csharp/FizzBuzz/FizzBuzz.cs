using FizzBuzzExample.Actions;
using FizzBuzzExample.Library.Ints;
using FizzBuzzExample.Library.Texts;

namespace FizzBuzzExample
{
    public class FizzBuzz : IFizzBuzz
    {
        private readonly Int _current;
        private readonly IResponseAction _responseAction;

        public FizzBuzz(Int current) : this(current, new StrategyResponseAction()) { }
        public FizzBuzz(Int current, IResponseAction responseAction)
        {
            _current = current;
            _responseAction = responseAction;
        }

        public Text Response() => _responseAction.Act(_current);
    }

    public interface IFizzBuzz
    {
        Text Response();
    }
}