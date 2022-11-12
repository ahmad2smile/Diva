using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using SourceGenerators.Templates;
using SourceGenerators.Utils;
using SourceGeneratorsAttributes;

namespace SourceGenerators;

[Generator]
public class RestApiSourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var classDeclarationsWithAttrsProvider =
            DeclarationsUtils.CreateSyntaxProvider<ClassDeclarationSyntax, RestApiAttribute>(context);
        var declarationsCombinedCompilation =
            context.CompilationProvider.Combine(classDeclarationsWithAttrsProvider.Collect());

        context.RegisterSourceOutput(declarationsCombinedCompilation, Execute);
    }

    private static void Execute(SourceProductionContext ctx,
        (Compilation Left, ImmutableArray<(ClassDeclarationSyntax syntax, AttributeSyntax attribute)> Right) arg2)
    {

        var (compilation, classDeclarationsWithAttrs) = arg2;

        foreach (var (syntax, attribute) in classDeclarationsWithAttrs)
        {
            var symbol = compilation.GetSemanticModel(syntax.SyntaxTree).GetDeclaredSymbol(syntax);

            if (symbol is null)
            {
                continue;
            }

            var controllerSource = TemplatesUtils.TemplateFileToString("NameController", "");
            var typeName = syntax.Identifier.ToString();
            var typeNamespaceName = symbol.ContainingNamespace.ToDisplayString();

            var controllerName = $"{typeName}Controller";
            var source = controllerSource.Replace("NameController", controllerName)
                .Replace(nameof(PlaceholderModel), typeName)
                .Replace("namespace SourceGenerators.Templates.RestApi;", $"namespace {typeNamespaceName};")
                .Replace("// ", "");

            ctx.AddSource($"{controllerName}.g.cs", SourceText.From(source, Encoding.UTF8));
        }
    }
}