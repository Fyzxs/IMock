using FizzBuzzExample.Library.Ints;
using FizzBuzzExample.Library.Texts;

namespace FizzBuzzExample.Actions
{
    public class NumberResponseAction : IResponseAction
    {
        public Text Act(Int value) => new NumberText(value);
    }
}