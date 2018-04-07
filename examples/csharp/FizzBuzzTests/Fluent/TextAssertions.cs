using FizzBuzzExample.Library.Texts;
using FluentAssertions;
using FluentAssertions.Primitives;

namespace FizzBuzzExampleTests.Fluent {
    public class TextAssertions : ReferenceTypeAssertions<Text, TextAssertions>
    {
        public TextAssertions(Text text) => Subject = text;

        protected override string Identifier => "Text";

        public void Be(string expected, string because = "", params object[] becauseArgs)
        {
            ((string)Subject).Should().Be(expected, because, becauseArgs);
        }
    }
}