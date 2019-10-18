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
using System.Collections.Generic;
using System.Linq;
using JetBrains.Util.Extension;

namespace Fyzxs.IMockResharperPlugin
{
    public class BuildMockClassContents
    {
        public Action<ITextControl> ExecutePsiTransaction(ICSharpContextActionDataProvider dataProvider, ISolution solution, IClassLikeDeclaration classDeclaration, IEnumerable<IInterface> interfaces)
        {
            IInterface[] interfacesArray = interfaces.ToArray();
            IInterface firstInterface = interfacesArray[0];
            string typeParameters = firstInterface.TypeParameters.AggregateString(",", (builder, parameter) => builder.Append(parameter.ShortName));
            if (firstInterface.TypeParameters.Count != 0) typeParameters = "<" + typeParameters + ">";
            string className = classDeclaration.DeclaredName;

            classDeclaration.AddClassMemberDeclaration((IClassMemberDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration($"private {className}(){{}}"));

            IClassLikeDeclaration builderClass = BuildMethod(dataProvider, interfacesArray, className, typeParameters);

            ProcessInterface(dataProvider, classDeclaration, interfacesArray, builderClass);

            classDeclaration.AddClassMemberDeclaration(builderClass);

            return textControl =>
            {
                IntentionTemplateUtil.ExecuteTemplate(solution, textControl, DocumentRange.InvalidRange);
            };
        }

        private void ProcessInterface(ICSharpContextActionDataProvider dataProvider, IClassLikeDeclaration classDeclaration, ICollection<IInterface> interfacesArray, IClassLikeDeclaration builderClass)
        {
            if (interfacesArray.IsEmpty()) return;

            foreach (IInterface theInterface in interfacesArray)
            {
                ProcessInterface(dataProvider, classDeclaration, theInterface.GetSuperTypes().Select(x => x.GetTypeElement()).OfType<IInterface>().ToArray(), builderClass);

                foreach (IEvent theEvent in theInterface.Events)
                {
                    string eventShortName = theEvent.ShortName;
                    string eventTypePresentableName = theEvent.Type.GetPresentableName(Languages.Instance.GetLanguageByName("CSHARP"));
                    classDeclaration.AddClassMemberDeclaration((IClassMemberDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration($"public event {eventTypePresentableName} _{eventShortName};"));
                    if (theEvent.Type.GetPresentableName(Languages.Instance.GetLanguageByName("CSHARP")) == "EventHandler")
                    {
                        classDeclaration.AddClassMemberDeclaration((IClassMemberDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration($"public void Trigger{eventShortName}() => _{eventShortName}.Invoke(this, EventArgs.Empty);"));
                    }
                    else
                    {
                        //type.GetScalarType().GetTypeElement().TypeParameters[0]
                        //ITypeParameter x = theEvent.Type.GetScalarType().GetTypeElement().TypeParameters[0];
                        //DeclaredTypeBase con = theEvent.Type as DeclaredTypeFromReferenceName;
                        //classDeclaration.AddClassMemberDeclaration((IClassMemberDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration($"public void Trigger{eventShortName}" +
                        //$"({x.ShortName} val) => _{eventShortName}.Invoke(val);"));
                    }

                }

                foreach (IMethod node in theInterface.Methods)
                {
                    ISignature methodSig = new MockMethod(node, theInterface);
                    classDeclaration.AddClassMemberDeclaration((IClassMemberDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration(methodSig.Signature()));
                    classDeclaration.AddClassMemberDeclaration((IClassMemberDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration(MockClassVariable(node, theInterface).Declaration()));
                }

                foreach (IMethod node in theInterface.Methods)
                {
                    builderClass.AddClassMemberDeclaration((IClassMemberDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration(MockClassVariable(node, theInterface).BuilderDeclaration()));
                    builderClass.AddClassMemberDeclaration((IClassMemberDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration(MockBuilderMethod(node, theInterface).Typed()));
                    builderClass.AddClassMemberDeclaration((IClassMemberDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration(MockBuilderMethod(node, theInterface).Lambdad()));
                }

                foreach (IMethod node in theInterface.Methods)
                {
                    ISignature2 methodSig = MockAssertMethod(node, theInterface);
                    methodSig.Signature(classDeclaration, dataProvider.ElementFactory);
                }
            }
        }

        private static IClassLikeDeclaration BuildMethod(ICSharpContextActionDataProvider dataProvider, ICollection<IInterface> interfacesArray, string className, string typeParameters)
        {
            IClassLikeDeclaration builderClass = (IClassLikeDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration("public sealed class Builder {}");
            //List<IInterface> interfaces = theInterface.GetSuperTypes().Select(x => x.GetTypeElement()).OfType<IInterface>().ToList();

            string allNodes = AllNodes(interfacesArray.ToArray());
            allNodes = allNodes.Replace("," + Environment.NewLine + ",", ",");
            builderClass.AddClassMemberDeclaration((IClassMemberDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration(
                $@"public {className}{typeParameters} Build(){{
    return new {className}{typeParameters}{{
            {allNodes}
    }};
}}"));
            return builderClass;
        }

        private static string AllNodes(ICollection<IInterface> interfacesArray)
        {
            string allNodes = "";
            if (interfacesArray.IsEmpty()) return allNodes;

            foreach (IInterface theInterface in interfacesArray)
            {
                allNodes += AllNodes(theInterface.GetSuperTypes().Select(x => x.GetTypeElement()).OfType<IInterface>().ToArray());
                
                allNodes += theInterface.Methods.AggregateString("," + Environment.NewLine, (sb, d) =>
                {
                    string name = $"_{new MethodName(d, theInterface).CamelCaseUnique()}";
                    return sb.Append($"{name} = {name}");
                });
                if (theInterface.Methods.Any())
                {
                    allNodes += "," + Environment.NewLine;
                }
            }

            return allNodes;
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

            if (!(method.ReturnType.IsVoid() || method.ReturnType.IsTask()) && method.Parameters.Any())
            {
                return new MockMethodWithParamAndResponseAsserts(method, theInterface);
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
    public class MockMethodWithParamAndResponseAsserts : ISignature2
    {
        private readonly ISignature2 _mockMethodResponseAsserts;
        private readonly ISignature2 _mockMethodParamAsserts;

        public MockMethodWithParamAndResponseAsserts(IMethod methodDeclaration, IInterface theInterface) :
            this(new MockMethodResponseAsserts(methodDeclaration, theInterface), new MockMethodParamAsserts(methodDeclaration, theInterface))
        { }

        private MockMethodWithParamAndResponseAsserts(ISignature2 mockMethodResponseAsserts, ISignature2 mockMethodParamAsserts)
        {
            _mockMethodResponseAsserts = mockMethodResponseAsserts;
            _mockMethodParamAsserts = mockMethodParamAsserts;
        }

        public void Signature(IClassLikeDeclaration classDeclaration, CSharpElementFactory dataProviderElementFactory)
        {
            _mockMethodParamAsserts.Signature(classDeclaration, dataProviderElementFactory);
            _mockMethodResponseAsserts.Signature(classDeclaration, dataProviderElementFactory);
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