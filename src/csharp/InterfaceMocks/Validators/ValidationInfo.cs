using System;
using System.Linq;
using System.Reflection;

namespace InterfaceMocks.Validators
{
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
            obj.Should().NotBeNull($"[name = {_name}] [type = {_type.Name}]");
            obj.Should().BeOfType(_type);
        }

        public FieldInfo FieldInfo(object obj)
        {
            FieldInfo fieldInfo = obj.GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(t => NameMatches(t.Name));
            fieldInfo.Should().NotBeNull($"[name = {_name}] for [type = {_type.Name}]");
            return fieldInfo;
        }

        public FieldInfo FieldInfo<T>()
        {
            FieldInfo fieldInfo = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(t => NameMatches(t.Name));
            fieldInfo.Should().NotBeNull($"[name = {_name}] for [type = {_type.Name}]");
            return fieldInfo;
        }
    }
}