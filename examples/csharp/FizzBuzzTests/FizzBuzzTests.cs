using FizzBuzzExample;
using FizzBuzzExample.Library.Bools;
using FizzBuzzExample.Library.Ints;
using FizzBuzzExample.Library.Texts;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FizzBuzzExampleTests
{
    [TestClass]
    public class FizzBuzzTests
    {
        [TestMethod, TestCategory("integration")]
        public void GivenInt_ShouldReturnTextOfInt()
        {
            //Arrange
            Bool isEqual = new FizzBuzz(new IntOf(1)).Response().IsEqual(new TextOf("1"));

            //Act
            bool actual = isEqual;

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod, TestCategory("integration")]
        public void GivenMultipleOfThree_ShouldReturnTextOfFizz()
        {
            //Arrange
            Bool isEqual = new FizzBuzz(new IntOf(6)).Response().IsEqual(new TextOf("Fizz"));

            //Act
            bool actual = isEqual;

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod, TestCategory("integration")]
        public void GivenMultipleOfFive_ShouldReturnTextOfBuzz()
        {
            //Arrange
            Bool isEqual = new FizzBuzz(new IntOf(10)).Response().IsEqual(new TextOf("Buzz"));

            //Act
            bool actual = isEqual;

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod, TestCategory("integration")]
        public void GivenMultipleOfThreeAndFive_ShouldReturnTextOfFizzBuzz()
        {
            //Arrange
            Bool isEqual = new FizzBuzz(new IntOf(30)).Response().IsEqual(new TextOf("FizzBuzz"));

            //Act
            bool actual = isEqual;

            //Assert
            actual.Should().BeTrue();
        }
    }
}
