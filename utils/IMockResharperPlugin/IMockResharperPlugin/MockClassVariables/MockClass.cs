using JetBrains.ReSharper.Psi;

namespace Fyzxs.IMockResharperPlugin.MockClassVariables
{
    public class MockClass : IMockClass
    {
        private readonly IInterface _theInterface;

        public MockClass(IInterface theInterface) => _theInterface = theInterface;

        public string Name() => $"Mock{_theInterface.ShortName.Substring(1)}";
    }

    public interface IMockClass
    {
        string Name();
    }
}