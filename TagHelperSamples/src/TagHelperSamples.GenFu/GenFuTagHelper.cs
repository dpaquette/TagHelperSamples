using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GenFu;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.VisualStudio.Web.CodeGeneration.DotNet;
using Microsoft.DotNet.ProjectModel;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.ProjectModel;

namespace TagHelperSamples.GenFu
{
    [HtmlTargetElement(Attributes = "genfu")]
    public class GenFuTagHelper : TagHelper
    {
        private readonly IApplicationInfo _applicationInfo;
        private static readonly List<MetadataReference> _references;

        static GenFuTagHelper()
        {
            //TODO: Review with James. This stuff used to be wired up via DI but is not anymore. 
            //      It was also very slow so I had to put it in a static constructor. Doing this for every tag helper instance was too slow.            
            var projectContext = IProjectContext.CreateContextForEachFramework(Directory.GetCurrentDirectory(), null, new[] { PlatformServices.Default.Application.RuntimeFramework.FullName }).First();
            ApplicationInfo appInfo = new ApplicationInfo("TagHelperSamples.GenFu", PlatformServices.Default.Application.ApplicationBasePath);
            ILibraryExporter exporter = new LibraryExporter(projectContext, appInfo);
            _references = new List<MetadataReference>();
            var exports = exporter.GetAllExports();
            var metaDataRefs = exports.SelectMany(x => x.GetMetadataReferences()).ToList();
            foreach (var reference in metaDataRefs)
            {
                _references.Add(reference);
            }
        }        

        [HtmlAttributeName("genfu")]
        public string PropertyName { get; set; }

        [HtmlAttributeName("genfu-type")]
        public Type PropertyType { get; set; } = typeof(string);


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            try
            {
                ICodeGenAssemblyLoadContext accessor = new DefaultAssemblyLoadContext();

                var syntaxTrees = CSharpSyntaxTree.ParseText($"public class Fake {{ public {PropertyType.FullName} {PropertyName} {{ get; set;  }} }} ");
                var assemblyName = Guid.NewGuid().ToString();


                var compilation = CSharpCompilation.Create(assemblyName)
                    .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                    .AddReferences(_references)
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
                        assembly = accessor.LoadStream(stream, null);
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
            catch (Exception ex)
            {
                output.Content.SetContent("Whoops");
            }
        }
    }
}
