using JetBrains.ReSharper.Psi;

namespace Fyzxs.IFakeResharperPlugin.FakeClassVariables
{
    public class FakeMethodWithParamVariable : FakeVariable
    {
        private readonly IMethodArgs _methodArgs;

        public FakeMethodWithParamVariable(IMethod methodDeclaration, IInterface theInterface) :
            this(methodDeclaration, theInterface, new MethodArgs(methodDeclaration))
        { }

        public FakeMethodWithParamVariable(IMethod methodDeclaration, IInterface theInterface, IMethodArgs methodArgs) :
            base(methodDeclaration, theInterface) => _methodArgs = methodArgs;

        protected override string TypeDeclaration() => $"FakeMethodWithParam<{_methodArgs.Types()}>";
    }
}