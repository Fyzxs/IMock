using FizzBuzzExample.Library.Ints;
using FizzBuzzExample.Library.Texts;

namespace FizzBuzzExample.Actions
{
    public interface IResponseAction
    {
        Text Act(Int value);
    }
}