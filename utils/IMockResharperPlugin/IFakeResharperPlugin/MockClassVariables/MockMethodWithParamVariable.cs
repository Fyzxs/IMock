using JetBrains.ReSharper.Psi;

namespace Fyzxs.IMockResharperPlugin.MockClassVariables
{
    public class MockMethodWithParamVariable : MockVariable
    {
        private readonly IMethodArgs _methodArgs;

        public MockMethodWithParamVariable(IMethod methodDeclaration, IInterface theInterface) :
            this(methodDeclaration, theInterface, new MethodArgs(methodDeclaration))
        { }

        public MockMethodWithParamVariable(IMethod methodDeclaration, IInterface theInterface, IMethodArgs methodArgs) :
            base(methodDeclaration, theInterface) => _methodArgs = methodArgs;

        protected override string TypeDeclaration() => $"MockMethodWithParam<{_methodArgs.Types()}>";
    }
}