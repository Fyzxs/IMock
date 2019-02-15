using JetBrains.ReSharper.Psi;

namespace Fyzxs.IMockResharperPlugin.MockClassVariables
{
    public class MockMethodVariable : MockVariable
    {
        public MockMethodVariable(IMethod methodDeclaration, IInterface theInterface) :
            base(methodDeclaration, theInterface)
        { }

        protected override string TypeDeclaration() => "MockMethod";
    }
}