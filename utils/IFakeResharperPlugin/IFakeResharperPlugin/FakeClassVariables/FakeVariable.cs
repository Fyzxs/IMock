using JetBrains.ReSharper.Psi;

namespace Fyzxs.IFakeResharperPlugin.FakeClassVariables
{
    public abstract class FakeVariable : IVariable
    {
        private readonly IMethodName _methodName;
        private readonly IFakeClass _fakeClass;
        protected FakeVariable(IMethod methodDeclaration, IInterface theInterface) :
            this(new MethodName(methodDeclaration, theInterface), new FakeClass(theInterface))
        { }

        protected FakeVariable(IMethodName methodName, IFakeClass fakeClass)
        {
            _methodName = methodName;
            _fakeClass = fakeClass;
        }

        public string Declaration() => $"private {Signature()};";

        public string BuilderDeclaration() => $"private readonly {Signature()} = new {TypeDeclaration()}(\"{_fakeClass.Name()}#{_methodName.Unique()}\");";

        protected string Signature() => $"{TypeDeclaration()} _{_methodName.CamelCaseUnique()}";

        protected abstract string TypeDeclaration();
    }
}