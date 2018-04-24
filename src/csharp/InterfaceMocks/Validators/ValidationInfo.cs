using InterfaceMocks.Exceptions;
using System;
using System.Linq;
using System.Reflection;

namespace InterfaceMocks.Validators
{
    internal sealed class ValidationInfo
    {
        private readonly string _name;
        private readonly Type _type;
        private readonly IAsserter _asserter;

        public ValidationInfo(string name, Type type) : this(name, type, new Asserter()) { }

        private ValidationInfo(string name, Type type, IAsserter asserter)
        {
            _name = name;
            _type = type;
            _asserter = asserter;
        }

        public bool NameMatches(string name) => name == _name;

        public void AssertType(object obj)
        {
            AssertIfNull(obj);
            _asserter.AssertIf(obj.GetType() != _type, $"Expected [name={_name}] to be of [type={_type.Name}] but found [type={obj.GetType().Name}]");
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

        private void AssertIfNull(object obj)
        {
            _asserter.AssertIf(obj == null, $"Expected [name={_name}] to be of [type={_type.Name}] but found null");
        }
    }
}