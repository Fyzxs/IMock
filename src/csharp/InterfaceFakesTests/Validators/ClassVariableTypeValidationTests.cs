using FluentAssertions;
using InterfaceFakes.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace InterfaceFakesTests.Validators
{
    [TestClass]
    public sealed class ClassVariableTypeValidationTests
    {
        [TestMethod, TestCategory("unit")]
        public void ShouldNotThrowWithValidVariableInBaseClass()
        {
            //Arrange
            ClassVariableTypeValidation subject = new ClassVariableTypeValidation()
                .FieldShouldBeType<string>("_value");

            //Act
            Action action = () => subject.AssertFieldsAreExpectedTypeInBaseClass<BaseClass>(new DerivedClass());

            //Assert
            action.Should().NotThrow();
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldThrowWithInvalidVariableNameInBaseClass()
        {
            //Arrange
            ClassVariableTypeValidation subject = new ClassVariableTypeValidation()
                .FieldShouldBeType<string>("_value2");

            //Act
            Action action = () => subject.AssertFieldsAreExpectedTypeInBaseClass<BaseClass>(new DerivedClass());

            //Assert
            action.Should().Throw<Exception>().WithMessage("Expected [name=_value2] to be of [type=string] but found null");
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldThrowWithInvalidVariableTypeInBaseClass()
        {
            //Arrange
            ClassVariableTypeValidation subject = new ClassVariableTypeValidation()
                .FieldShouldBeType<Action>("_value");

            //Act
            Action action = () => subject.AssertFieldsAreExpectedTypeInBaseClass<BaseClass>(new DerivedClass());

            //Assert
            action.Should().Throw<Exception>().WithMessage("Expected [name=_value] to be of [type=Action] but found [type=String]");
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldNotThrowWithValidVariable()
        {
            //Arrange
            ClassVariableTypeValidation subject = new ClassVariableTypeValidation()
                .FieldShouldBeType<string>("_value");

            //Act
            Action action = () => subject.AssertFieldsAreExpectedType(new VariabledClass());

            //Assert
            action.Should().NotThrow();
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldThrowWithInvalidVariableName()
        {
            //Arrange
            ClassVariableTypeValidation subject = new ClassVariableTypeValidation()
                .FieldShouldBeType<string>("_value2");

            //Act
            Action action = () => subject.AssertFieldsAreExpectedType(new VariabledClass());

            //Assert
            action.Should().Throw<Exception>().WithMessage("Expected [name=_value2] to be of [type=string] but found null");
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldThrowWithInvalidVariableType()
        {
            //Arrange
            ClassVariableTypeValidation subject = new ClassVariableTypeValidation()
                .FieldShouldBeType<Action>("_value");

            //Act
            Action action = () => subject.AssertFieldsAreExpectedType(new VariabledClass());

            //Assert
            action.Should().Throw<Exception>().WithMessage("Expected [name=_value] to be of [type=Action] but found [type=String]");
        }

        private sealed class VariabledClass
        {
            private readonly string _value;

            public VariabledClass() : this("string") { }

            private VariabledClass(string value) => _value = value;
        }

        private abstract class BaseClass
        {
            private readonly string _value;

            protected BaseClass(string value) => _value = value;
        }

        private sealed class DerivedClass : BaseClass
        {
            public DerivedClass():base("string"){}
        }
    }
}