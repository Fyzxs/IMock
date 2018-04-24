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
            _asserter.AssertIf(obj != null, $"[name = {_name}] [type = {_type.Name}]");
            _asserter.AssertIf(obj.GetType() == _type, $"[obj={obj} was not of the expected [type={_type.Name}");
        }

        public FieldInfo FieldInfo(object obj)
        {
            FieldInfo fieldInfo = obj.GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(t => NameMatches(t.Name));

            _asserter.AssertIf(fieldInfo != null, $"[name = {_name}] [type = {_type.Name}]");
            return fieldInfo;
        }

        public FieldInfo FieldInfo<T>()
        {
            FieldInfo fieldInfo = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(t => NameMatches(t.Name));
            _asserter.AssertIf(fieldInfo != null, $"[name = {_name}] [type = {_type.Name}]");
            return fieldInfo;
        }
    }
}