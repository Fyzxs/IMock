using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace Fyzxs.IMockResharperPlugin.MockClassVariables
{
    public class MockClass : IMockClass
    {
        private readonly IClassLikeDeclaration _theInterface;

        public MockClass(IClassLikeDeclaration theInterface) => _theInterface = theInterface;

        public string Name() => $"Mock{_theInterface.DeclaredElement.ShortName.Substring(1)}";
    }

    public interface IMockClass
    {
        string Name();
    }
}