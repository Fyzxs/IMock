using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace Fyzxs.IMockResharperPlugin.MockClassVariables {
    public abstract class MockVariable : IVariable
    {
        private readonly IMethodName _methodName;
        private readonly IMockClass _mockClass;
        protected MockVariable(IMethodDeclaration methodDeclaration, IClassLikeDeclaration theInterface) :
            this(new MethodName(methodDeclaration, theInterface), new MockClass(theInterface))
        { }

        protected MockVariable(IMethodName methodName, IMockClass mockClass)
        {
            _methodName = methodName;
            _mockClass = mockClass;
        }

        public string Declaration() => $"private {Signature()};";

        public string BuilderDeclaration() => $"private readonly {Signature()} = new {TypeDeclaration()}(\"{_mockClass.Name()}#{_methodName.Unique()}\");";

        protected string Signature() => $"{TypeDeclaration()} _{_methodName.CamelCaseUnique()}";

        protected abstract string TypeDeclaration();
    }
}