using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

namespace NamespaceFormatAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class NamespaceFormatAnalyzerAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "NamespaceFormatAnalyzer";

        // You can change these strings in the Resources.resx file. If you do not want your analyzer to be localize-able, you can use regular strings for Title and MessageFormat.
        // See https://github.com/dotnet/roslyn/blob/main/docs/analyzers/Localizing%20Analyzers.md for more on localization
        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Naming";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            context.RegisterSyntaxNodeAction(syntaxContext => AnalyzeSyntax(syntaxContext, new NamespaceFormatValidator()), SyntaxKind.NamespaceDeclaration);
        }

        private static void AnalyzeSyntax(SyntaxNodeAnalysisContext context, NamespaceFormatValidator validator)
        {
            var namespaceSyntax = (NamespaceDeclarationSyntax)context.Node;

            if (!validator.IsValid(namespaceSyntax.Name.ToString()))
            {
                // For all such symbols, produce a diagnostic.
                var diagnostic = Diagnostic.Create(Rule, namespaceSyntax.Name.GetLocation(), namespaceSyntax.Name.ToString());

                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
