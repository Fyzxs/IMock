using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace Fyzxs.IMockResharperPlugin.MockClassVariables
{
    public class MockMethodWithParamVariable : MockVariable
    {
        private readonly IMethodArgs _methodArgs;

        public MockMethodWithParamVariable(IMethodDeclaration methodDeclaration, IClassLikeDeclaration theInterface) :
            this(methodDeclaration, theInterface, new MethodArgs(methodDeclaration))
        { }

        public MockMethodWithParamVariable(IMethodDeclaration methodDeclaration, IClassLikeDeclaration theInterface, IMethodArgs methodArgs) :
            base(methodDeclaration, theInterface) => _methodArgs = methodArgs;

        protected override string TypeDeclaration() => $"MockMethodWithParam<{_methodArgs.Types()}>";
    }
}