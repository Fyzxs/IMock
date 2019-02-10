using FizzBuzzExample.Actions;
using FizzBuzzExample.Library.Texts;
using FizzBuzzExampleTests.Fluent;
using FizzBuzzExampleTests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FizzBuzzExampleTests.Actions
{
    [TestClass]
    public class NumberResponseActionTests
    {
        [TestMethod, TestCategory("unit")]
        public void ShouldReturnNumberText()
        {
            //Arrange
            FakeInt fakeInt = new FakeInt.Builder().RawValue(15).Build();
            NumberResponseAction subject = new NumberResponseAction();

            //Act
            Text actual = subject.Act(fakeInt);

            //Assert
            actual.Should().Be("15");
        }
    }
}