using Fyzxs.IMockResharperPlugin.MockClassVariables;
using JetBrains.DocumentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.CSharp.Analyses.Bulbs;
using JetBrains.ReSharper.Intentions.Util;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.TextControl;
using JetBrains.Util;
using System;

namespace Fyzxs.IMockResharperPlugin
{
    public class BuildMockClassContents
    {
        public Action<ITextControl> ExecutePsiTransaction(ICSharpContextActionDataProvider dataProvider, ISolution solution, IClassLikeDeclaration classDeclaration, IClassLikeDeclaration theInterface)
        {
            //IClassLikeDeclaration theInterface = ((IClassLikeDeclaration)dataProvider.GetSelectedElement<IInterfaceDeclaration>()).NotNull();
            ITypeElement typeElement = theInterface.DeclaredElement;
            string typeParameters = typeElement.TypeParameters.AggregateString(",", (builder, parameter) => builder.Append(parameter.ShortName));
            if (typeElement.TypeParameters.Count != 0) typeParameters = "<" + typeParameters + ">";
            string interfaceName = typeElement.ShortName;
            string className = $"Mock{interfaceName.Substring(1)}";
            //IClassLikeDeclaration classDeclaration = (IClassLikeDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration($"public partial class {className}{typeParameters} : {interfaceName}{typeParameters} {{}}");

            ICSharpTypeAndNamespaceHolderDeclaration holderDeclaration = (ICSharpTypeAndNamespaceHolderDeclaration)(CSharpNamespaceDeclarationNavigator.GetByTypeDeclaration(theInterface) ?? (object)CSharpFileNavigator.GetByTypeDeclaration(theInterface));

            classDeclaration = (IClassLikeDeclaration)holderDeclaration.AddTypeDeclarationAfter(classDeclaration, theInterface);

            classDeclaration.AddClassMemberDeclaration((IClassMemberDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration($"private {className}(){{}}"));
            foreach (IMethodDeclaration node in theInterface.MethodDeclarations)
            {
                ISignature methodSig = new MockMethod(node, theInterface);
                classDeclaration.AddClassMemberDeclaration((IClassMemberDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration(methodSig.Signature()));
                classDeclaration.AddClassMemberDeclaration((IClassMemberDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration(MockClassVariable(node, theInterface).Declaration()));
            }
            IClassLikeDeclaration builderClass = (IClassLikeDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration("public class Builder {}");

            string allNodes = theInterface.MethodDeclarations.AggregateString("," + Environment.NewLine, (sb, d) =>
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
            foreach (IMethodDeclaration node in theInterface.MethodDeclarations)
            {

                builderClass.AddClassMemberDeclaration((IClassMemberDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration(MockClassVariable(node, theInterface).BuilderDeclaration()));
                builderClass.AddClassMemberDeclaration((IClassMemberDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration(MockBuilderMethod(node, theInterface).Typed()));
                builderClass.AddClassMemberDeclaration((IClassMemberDeclaration)dataProvider.ElementFactory.CreateTypeMemberDeclaration(MockBuilderMethod(node, theInterface).Lambdad()));
            }
            classDeclaration.AddClassMemberDeclaration(builderClass);
            return textControl =>
            {
                IntentionTemplateUtil.ExecuteTemplate(solution, textControl, DocumentRange.InvalidRange);
            };
        }

        //TODO turn this into a chain
        private IVariable MockClassVariable(IMethodDeclaration node, IClassLikeDeclaration theInterface)
        {
            if ((node.Type.IsVoid() || node.Type.IsTask()) && node.Params.ParameterDeclarations.IsEmpty())
            {
                return new MockMethodVariable(node, theInterface);
            }

            if ((node.Type.IsVoid() || node.Type.IsTask()) && node.Params.ParameterDeclarations.Any())
            {
                return new MockMethodWithParamVariable(node, theInterface);
            }

            if (!(node.Type.IsVoid() || node.Type.IsTask()) && node.Params.ParameterDeclarations.Any())
            {
                return new MockMethodWithParamAndResponseVariable(node, theInterface);
            }

            if (!(node.Type.IsVoid() || node.Type.IsTask()) && node.Params.ParameterDeclarations.IsEmpty())
            {
                return new MockMethodWithResponseVariable(node, theInterface);
            }

            throw new NotSupportedException("You're not supposed to be here");
        }

        //TODO turn this into a chain
        private IMockBuilderMethods MockBuilderMethod(IMethodDeclaration node, IClassLikeDeclaration theInterface)
        {
            if (node.Type.IsVoid() || node.Type.IsTask())
            {
                return new MockActionBuilderMethods(node, theInterface);
            }
            if (!(node.Type.IsVoid() || node.Type.IsTask()))
            {
                return new MockResponseBuilderMethods(node, theInterface);
            }

            throw new NotSupportedException("You're not supposed to be here");
        }
    }
}