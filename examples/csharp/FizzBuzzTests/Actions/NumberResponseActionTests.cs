using FizzBuzzExample.Actions;
using FizzBuzzExample.Library.Texts;
using FizzBuzzExampleTests.Fluent;
using FizzBuzzExampleTests.Mocks;
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
            MockInt mockInt = new MockInt.Builder().RawValue(15).Build();
            NumberResponseAction subject = new NumberResponseAction();

            //Act
            Text actual = subject.Act(mockInt);

            //Assert
            actual.Should().Be("15");
        }
    }
}