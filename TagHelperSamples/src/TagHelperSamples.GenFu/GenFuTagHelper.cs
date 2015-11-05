using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GenFu;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.Dnx.Compilation;
using Microsoft.Dnx.Compilation.CSharp;
using Microsoft.Dnx.Runtime;

namespace TagHelperSamples.GenFu
{
    [HtmlTargetElement(Attributes = "genfu")]
    public class GenFuTagHelper : TagHelper
    {
        private readonly ILibraryExporter _exporter;
        private readonly IAssemblyLoadContextAccessor _accessor;

        [HtmlAttributeName("genfu")]
        public string PropertyName { get; set; }

        [HtmlAttributeName("genfu-type")]
        public Type PropertyType { get; set; } = typeof(string);

        public Dictionary<string, Type> genfud { get; set; }


        public GenFuTagHelper(ILibraryExporter exporter, IAssemblyLoadContextAccessor accessor)
        {
            _exporter = exporter;
            _accessor = accessor;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            try
            {
                var syntaxTrees = CSharpSyntaxTree.ParseText($"public class Fake {{ public string {PropertyName} {{ get; set;  }} }} ");
                var assemblyName = Guid.NewGuid().ToString();
                var references = new List<MetadataReference>();
                var export = _exporter.GetAllExports("TagHelperSamples.GenFu");
                foreach (var reference in export.MetadataReferences)
                {
                    references.Add(reference.ConvertMetadataReference(MetadataReferenceExtensions.CreateAssemblyMetadata));
                }

                var compilation = CSharpCompilation.Create(assemblyName)
                    .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                    .AddReferences(references)
                    .AddSyntaxTrees(syntaxTrees);

                Assembly assembly = null;

                using (var stream = new MemoryStream())
                {
                    // this is broked...
                    EmitResult compileResult = compilation.Emit(stream);
                    // we get here, with diagnostic errors (check compileResult.Diagnostics)
                    if (compileResult.Success)
                    {
                        stream.Position = 0;
                        assembly = _accessor.Default.LoadStream(stream, null);
                    }
                }
                
                // iterate over the types in the assembly
                var types = assembly?.GetExportedTypes();
                if (types?.Length == 1)
                {
                    Type fooness = types[0];
                    var result = A.New(fooness);

                    var propValue = result.GetType().GetProperty(PropertyName).GetValue(result, null).ToString();
                    output.Content.SetContent(propValue);
                }
            }
            catch (Exception)
            {
                output.Content.SetContent("Whoops");
            }
        }
    }
}
