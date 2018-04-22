using Fyzxs.IMockResharperPlugin.MockClassVariables;
using JetBrains.DocumentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.CSharp.Analyses.Bulbs;
using JetBrains.ReSharper.Intentions.Util;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.TextControl;
using JetBrains.Util;
using System;

namespace Fyzxs.IMockResharperPlugin
{
    public class BuildMockClassContents
    {
        public Action<ITextControl> ExecutePsiTransaction(ICSharpContextActionDataProvider dataProvider, ISolution solution, IClassLikeDeclaration classDeclaration, IInterface theInterface)
        {
            string typeParameters = theInterface.TypeParameters.AggregateString(",", (builder, parameter) => builder.Append(parameter.ShortName));
            if (theInterface.TypeParameters.Count != 0) typeParameters = "<" + typeParameters + ">";
            string interfaceName = theInterface.ShortName;
            string className = $"Mock{interfaceName.Substring(1)}";

            classDeclaration.AddClassMemberDeclaration((IClassMemberDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration($"private {className}(){{}}"));
            foreach (IMethod node in theInterface.Methods)
            {
                ISignature methodSig = new MockMethod(node, theInterface);
                classDeclaration.AddClassMemberDeclaration((IClassMemberDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration(methodSig.Signature()));
                classDeclaration.AddClassMemberDeclaration((IClassMemberDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration(MockClassVariable(node, theInterface).Declaration()));
            }

            IClassLikeDeclaration builderClass = (IClassLikeDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration("public class Builder {}");
            BuildMethod(dataProvider, theInterface, builderClass, className, typeParameters);

            foreach (IMethod node in theInterface.Methods)
            {

                builderClass.AddClassMemberDeclaration((IClassMemberDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration(MockClassVariable(node, theInterface).BuilderDeclaration()));
                builderClass.AddClassMemberDeclaration((IClassMemberDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration(MockBuilderMethod(node, theInterface).Typed()));
                builderClass.AddClassMemberDeclaration((IClassMemberDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration(MockBuilderMethod(node, theInterface).Lambdad()));
            }
            classDeclaration.AddClassMemberDeclaration(builderClass);

            foreach (IMethod node in theInterface.Methods)
            {
                ISignature2 methodSig = MockAssertMethod(node, theInterface);
                methodSig.Signature(classDeclaration, dataProvider.ElementFactory);
            }
            return textControl =>
            {
                IntentionTemplateUtil.ExecuteTemplate(solution, textControl, DocumentRange.InvalidRange);
            };
        }

        private static void BuildMethod(ICSharpContextActionDataProvider dataProvider, IInterface theInterface, IClassLikeDeclaration builderClass, string className, string typeParameters)
        {
            string allNodes = theInterface.Methods.AggregateString("," + Environment.NewLine, (sb, d) =>
            {
                string name = $"_{new MethodName(d, theInterface).CamelCaseUnique()}";
                return sb.Append($"{name} = {name}");
            });
            builderClass.AddClassMemberDeclaration((IClassMemberDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration(
                $@"public {className}{typeParameters} Build(){{
    return new {className}{typeParameters}{{
            {allNodes}
    }};
}}"));
        }

        //TODO turn this into a chain
        private IVariable MockClassVariable(IMethod method, IInterface theInterface)
        {
            if ((method.ReturnType.IsVoid() || method.ReturnType.IsTask()) && method.Parameters.IsEmpty())
            {
                return new MockMethodVariable(method, theInterface);
            }

            if ((method.ReturnType.IsVoid() || method.ReturnType.IsTask()) && method.Parameters.Any())
            {
                return new MockMethodWithParamVariable(method, theInterface);
            }

            if (!(method.ReturnType.IsVoid() || method.ReturnType.IsTask()) && method.Parameters.Any())
            {
                return new MockMethodWithParamAndResponseVariable(method, theInterface);
            }

            if (!(method.ReturnType.IsVoid() || method.ReturnType.IsTask()) && method.Parameters.IsEmpty())
            {
                return new MockMethodWithResponseVariable(method, theInterface);
            }

            throw new NotSupportedException("You're not supposed to be here");
        }

        //TODO turn this into a chain
        private IMockBuilderMethods MockBuilderMethod(IMethod method, IInterface theInterface)
        {
            if (method.ReturnType.IsVoid() || method.ReturnType.IsTask())
            {
                return new MockActionBuilderMethods(method, theInterface);
            }
            if (!(method.ReturnType.IsVoid() || method.ReturnType.IsTask()))
            {
                return new MockResponseBuilderMethods(method, theInterface);
            }

            throw new NotSupportedException("You're not supposed to be here");
        }
        //TODO turn this into a chain
        private ISignature2 MockAssertMethod(IMethod method, IInterface theInterface)
        {
            if (method.Parameters.IsEmpty())
            {
                return new MockMethodAsserts(method, theInterface);
            }

            if (method.ReturnType.IsVoid() || method.ReturnType.IsTask())
            {
                return new MockMethodParamAsserts(method, theInterface);
            }

            if (!(method.ReturnType.IsVoid() || method.ReturnType.IsTask()))
            {
                return new MockMethodResponseAsserts(method, theInterface);
            }

            throw new NotSupportedException("You're not supposed to be here");
        }
    }

    public interface ISignature2
    {
        void Signature(IClassLikeDeclaration classDeclaration, CSharpElementFactory dataProviderElementFactory);
    }
    public class MockMethodResponseAsserts : ISignature2
    {
        private readonly PsiLanguageType _languageType = Languages.Instance.GetLanguageByName("CSHARP");
        private readonly IMethod _methodDeclaration;
        private readonly IMethodName _methodName;

        public MockMethodResponseAsserts(IMethod methodDeclaration, IInterface theInterface) :
            this(methodDeclaration, new MethodName(methodDeclaration, theInterface), new MethodArgs(methodDeclaration))
        { }

        public MockMethodResponseAsserts(IMethod methodDeclaration, IMethodName methodName, IMethodArgs methodArgs)
        {
            _methodDeclaration = methodDeclaration;
            _methodName = methodName;
        }

        public void Signature(IClassLikeDeclaration classDeclaration, CSharpElementFactory dataProviderElementFactory)
        {
        }
    }

    public class MockMethodAsserts : ISignature2
    {
        private readonly PsiLanguageType _languageType = Languages.Instance.GetLanguageByName("CSHARP");
        private readonly IMethod _methodDeclaration;
        private readonly IMethodName _methodName;

        public MockMethodAsserts(IMethod methodDeclaration, IInterface theInterface) :
            this(methodDeclaration, new MethodName(methodDeclaration, theInterface), new MethodArgs(methodDeclaration))
        { }

        public MockMethodAsserts(IMethod methodDeclaration, IMethodName methodName, IMethodArgs methodArgs)
        {
            _methodDeclaration = methodDeclaration;
            _methodName = methodName;
        }
        private string Signature() =>
            $"public void Assert{_methodName.Unique()}Invoked() => _{_methodName.CamelCaseUnique()}.AssertInvoked();";

        public void Signature(IClassLikeDeclaration classDeclaration, CSharpElementFactory dataProviderElementFactory)
        {
            classDeclaration.AddClassMemberDeclaration((IClassMemberDeclaration)dataProviderElementFactory.CreateTypeMemberDeclaration(Signature()));
        }
    }

    public class MockMethodParamAsserts : ISignature2
    {
        private readonly PsiLanguageType _languageType = Languages.Instance.GetLanguageByName("CSHARP");
        private readonly IMethod _methodDeclaration;
        private readonly IMethodName _methodName;
        private readonly IMethodArgs _methodArgs;

        public MockMethodParamAsserts(IMethod methodDeclaration, IInterface theInterface) :
            this(methodDeclaration, new MethodName(methodDeclaration, theInterface), new MethodArgs(methodDeclaration))
        { }

        public MockMethodParamAsserts(IMethod methodDeclaration, IMethodName methodName, IMethodArgs methodArgs)
        {
            _methodDeclaration = methodDeclaration;
            _methodName = methodName;
            _methodArgs = methodArgs;
        }
        //public void AssertTupleInvokedWith(string arg1, int arg2) => _paramTuple.AssertInvokedWith(new Tuple<string, int>(arg1, arg2));
        private string Signature() =>
            $"public void Assert{_methodName.Unique()}InvokedWith({_methodArgs.Definition()}) => _{_methodName.CamelCaseUnique()}.AssertInvokedWith({_methodArgs.InvokedArgs()});";

        public void Signature(IClassLikeDeclaration classDeclaration, CSharpElementFactory dataProviderElementFactory)
        {
            classDeclaration.AddClassMemberDeclaration((IClassMemberDeclaration)dataProviderElementFactory.CreateTypeMemberDeclaration(Signature()));
        }
    }
}