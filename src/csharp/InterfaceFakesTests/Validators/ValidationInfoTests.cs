using FluentAssertions;
using InterfaceFakes.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace InterfaceFakesTests.Validators
{
    [TestClass]
    public class ValidationInfoTests
    {
        [TestMethod, TestCategory("unit")]
        public void NameMatches_ShouldReturnTrueGivenNameMatches()
        {
            //Arrange
            ValidationInfo subject = new ValidationInfo("_nameHere", typeof(string));

            //Act
            bool actual = subject.NameMatches("_nameHere");

            //Assert
            actual.Should().BeTrue();
        }
        [TestMethod, TestCategory("unit")]
        public void NameMatches_ShouldReturnFalseGivenNameDoesNotMatches()
        {
            //Arrange
            ValidationInfo subject = new ValidationInfo("_nameHere", typeof(string));

            //Act
            bool actual = subject.NameMatches("HAH");

            //Assert
            actual.Should().BeFalse();
        }
        [TestMethod, TestCategory("unit")]
        public void AssertType_ShouldThrowExceptionGivenNull()
        {
            //Arrange

            ValidationInfo subject = new ValidationInfo("_nameHere", typeof(string));

            //Act
            Action action = () => subject.AssertType(null);

            //Assert
            action.Should().Throw<Exception>().WithMessage("Expected [name=_nameHere] to be of [type=String] but found null");
        }


        [TestMethod, TestCategory("unit")]
        public void AssertType_ShouldThrowExceptionGivenNotExpectedType()
        {
            //Arrange

            ValidationInfo subject = new ValidationInfo("_nameHere", typeof(Example));

            //Act
            Action action = () => subject.AssertType("");

            //Assert
            action.Should().Throw<Exception>().WithMessage("Expected [name=_nameHere] to be of [type=Example] but found [type=String]");
        }


        [TestMethod, TestCategory("unit")]
        public void FieldInfo_ShouldReturnFieldsInfo()
        {
            //Arrange
            ValidationInfo subject = new ValidationInfo("_nameHere", typeof(string));

            //Act
            FieldInfo actual = subject.FieldInfo(new Example("blort"));

            //Assert
            actual.Should().NotBeNull();
            actual.Name.Should().Be("_nameHere");
        }


        [TestMethod, TestCategory("unit")]
        public void FieldInfo_ShouldThrowIfFieldNotFound()
        {
            //Arrange
            ValidationInfo subject = new ValidationInfo("_notFound", typeof(string));

            //Act
            Action actual = () => subject.FieldInfo(new Example("blort"));

            //Assert
            actual.Should().Throw<Exception>().WithMessage("Expected [name=_notFound] to be of [type=String] but found null");
        }



        [TestMethod, TestCategory("unit")]
        public void FieldInfoT_ShouldReturnFieldsInfo()
        {
            //Arrange
            ValidationInfo subject = new ValidationInfo("_nameHere", typeof(string));

            //Act
            FieldInfo actual = subject.FieldInfo<Example>();

            //Assert
            actual.Should().NotBeNull();
            actual.Name.Should().Be("_nameHere");
        }

        [TestMethod, TestCategory("unit")]
        public void FieldInfoT_ShouldThrowIfFieldNotFound()
        {
            //Arrange
            ValidationInfo subject = new ValidationInfo("_notFound", typeof(string));

            //Act
            Action actual = () => subject.FieldInfo<Example>();

            //Assert
            actual.Should().Throw<Exception>().WithMessage("Expected [name=_notFound] to be of [type=String] but found null");
        }


        [TestMethod, TestCategory("unit")]
        public void FieldInfoT_ShouldThrowIfFieldNotInClass()
        {
            //Arrange
            ValidationInfo subject = new ValidationInfo("_nameHere", typeof(string));

            //Act
            Action actual = () => subject.FieldInfo<ExampleChild>();

            //Assert
            actual.Should().Throw<Exception>().WithMessage("Expected [name=_nameHere] to be of [type=String] but found null");
        }

        private class Example
        {
            private readonly string _nameHere;

            public Example(string value)
            {
                _nameHere = value;
            }

            public string Getter() => _nameHere;
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        private class ExampleChild : Example
        {
            public ExampleChild() : base("superTimes") { }
        }
    }
}