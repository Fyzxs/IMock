using InterfaceMocks.Exceptions;
using System;
using System.Linq;
using System.Reflection;

namespace InterfaceMocks.Validators
{
    internal sealed class ValidationInfo
    {
        private static readonly object UndefinedExpectedValue = new object();
        private readonly string _name;
        private readonly Type _type;
        private readonly object _expectedValue;
        private readonly IAsserter _asserter;

        public ValidationInfo(string name, Type type) : this(name, type, UndefinedExpectedValue) { }
        public ValidationInfo(string name, Type type, object expectedValue) : this(name, type, expectedValue, new Asserter()) { }
        private ValidationInfo(string name, Type type, object expectedValue, IAsserter asserter)
        {
            _name = name;
            _type = type;
            _expectedValue = expectedValue;
            _asserter = asserter;
        }

        public bool NameMatches(string name) => name == _name;


        public void Assert(object actualValue)
        {
            AssertType(actualValue);
            AssertValue(actualValue);
        }
        private void AssertType(object actualValue)
        {
            AssertIfNull(actualValue);
            _asserter.AssertIf(actualValue.GetType() != _type, $"Expected [name={_name}] to be of [type={_type.Name}] but found [type={actualValue.GetType().Name}]");
        }

        private void AssertValue(object actualValue)
        {
            
            if (_expectedValue == UndefinedExpectedValue) return;
            AssertPrimitiveValue(actualValue);
            AssertReferenceValue(actualValue);
        }

        private void AssertReferenceValue(object actualValue)
        {
            if (ReferenceEquals(_expectedValue, actualValue)) return;
            _asserter.AssertIf(!_expectedValue.Equals(actualValue), $"Field [name={_name}] is not the same reference as expected and does not '#Equals()' actual. [expected={_expectedValue}] [actual  ={actualValue}]");
        }

        private void AssertPrimitiveValue(object actualValue)
        {
            if (!actualValue.GetType().IsPrimitive) return;
            _asserter.AssertIf(!_expectedValue.Equals(actualValue), $"Expected [name={_name}] to have a value of [expected={_expectedValue}] but found [actual={actualValue}].");
        }

        public FieldInfo FieldInfo(object obj)
        {
            FieldInfo fieldInfo = obj.GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(t => NameMatches(t.Name));

            AssertIfNull(fieldInfo);
            return fieldInfo;
        }

        public FieldInfo FieldInfo<T>()
        {
            FieldInfo fieldInfo = typeof(T)
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(t => NameMatches(t.Name));

            AssertIfNull(fieldInfo);
            return fieldInfo;
        }

        public void AssertIfNull(object obj)
        {
            _asserter.AssertIf(obj == null, $"Expected field [name={_name}] to be of [type={_type.Name}] but a field [name={_name}] was not found.");
        }
    }
}