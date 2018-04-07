using System;
using System.Linq;
using System.Reflection;
using FluentAssertions;

namespace FizzBuzzExampleTests.Fluent {
    public class ValidationInfo
    {
        private readonly string _name;
        private readonly Type _type;

        public ValidationInfo(string name, Type type)
        {
            _name = name;
            _type = type;
        }

        public bool NameMatches(string name) => name == _name;

        public void AssertType(object obj)
        {
            obj.Should().NotBeNull($"expected variable [name = {_name}]");
            obj.Should().BeOfType(_type);
        }

        public FieldInfo FieldInfo(object obj) => FieldInfo(obj.GetType());
        public FieldInfo FieldInfo<T>() => FieldInfo(typeof(T));

        private FieldInfo FieldInfo(IReflect type)
        {
            FieldInfo fieldInfo = type
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(t => NameMatches(t.Name));
            fieldInfo.Should().NotBeNull($"expected variable [name = {_name}]");
            return fieldInfo;
        }
    }
}