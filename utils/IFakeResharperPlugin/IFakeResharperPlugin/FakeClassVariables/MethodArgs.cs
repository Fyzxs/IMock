using JetBrains.ReSharper.Psi;
using JetBrains.Util;
using System;
using System.Text;

namespace Fyzxs.IFakeResharperPlugin.FakeClassVariables
{
    public class MethodArgs : IMethodArgs
    {
        private readonly IMethod _methodDeclaration;
        private readonly PsiLanguageType _languageType = Languages.Instance.GetLanguageByName("CSHARP");

        public MethodArgs(IMethod methodDeclaration) => _methodDeclaration = methodDeclaration;

        public string Definition() => AggregateString((builder, param) => builder.Append($"{ParamName(param)} {param.ShortName}"));

        public string Types() => MultipleParams() ? TupleDefinition() : TypeDefinition();

        private string TypeDefinition() => AggregateString((builder, param) => builder.Append(ParamName(param)));

        public string InvokedArgs() => MultipleParams()
            ? TupleArgValues()
            : SingleArgValues();

        private string TupleArgValues() => $"new {TupleDefinition()}({SingleArgValues()})";

        private string TupleDefinition() => $"Tuple<{TypeDefinition()}>";

        private bool MultipleParams() => 1 < _methodDeclaration.Parameters.Count;

        private string SingleArgValues() => AggregateString((builder, param) => builder.Append(param.ShortName));

        private string AggregateString(Func<StringBuilder, IParameter, StringBuilder> func) =>
            _methodDeclaration.Parameters.AggregateString(", ", func);

        private string ParamName(ITypeOwner param) => param.Type.GetPresentableName(_languageType);
    }
}