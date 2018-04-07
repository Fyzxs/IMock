using FizzBuzzExample.Library.Texts;

namespace FizzBuzzExampleTests.Fluent {
    public static class TextFluentAssertionsExtensions
    {
        public static TextAssertions Should(this Text origin) => new TextAssertions(origin);
    }
}