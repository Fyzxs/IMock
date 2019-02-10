using JetBrains.ReSharper.Psi;

namespace Fyzxs.IFakeResharperPlugin.FakeClassVariables
{
    public class FakeClass : IFakeClass
    {
        private readonly IInterface _theInterface;

        public FakeClass(IInterface theInterface) => _theInterface = theInterface;

        public string Name() => $"Fake{_theInterface.ShortName.Substring(1)}";
    }

    public interface IFakeClass
    {
        string Name();
    }
}