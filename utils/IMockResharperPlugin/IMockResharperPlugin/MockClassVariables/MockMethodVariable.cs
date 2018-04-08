using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace Fyzxs.IMockResharperPlugin.MockClassVariables
{
    public class MockMethodVariable : MockVariable
    {
        public MockMethodVariable(IMethodDeclaration methodDeclaration, IClassLikeDeclaration theInterface) :
            base(methodDeclaration, theInterface)
        { }

        protected override string TypeDeclaration() => "MockMethod";
    }
}