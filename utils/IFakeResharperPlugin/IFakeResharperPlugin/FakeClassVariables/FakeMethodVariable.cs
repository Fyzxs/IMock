using JetBrains.ReSharper.Psi;

namespace Fyzxs.IFakeResharperPlugin.FakeClassVariables
{
    public class FakeMethodVariable : FakeVariable
    {
        public FakeMethodVariable(IMethod methodDeclaration, IInterface theInterface) :
            base(methodDeclaration, theInterface)
        { }

        protected override string TypeDeclaration() => "FakeMethod";
    }
}