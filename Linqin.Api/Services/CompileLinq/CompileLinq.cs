using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Linqin.Api.Models;
using Newtonsoft.Json;
using System.Linq;

namespace LinqCompiler
{
  public static class Compiler
  {

    public static ResponsePost ExecuteString(string linqQuery, IEnumerable<ShapeModel> startCollection)
    {

      string code = "using System;" +
        "using System.IO;" +
        "using System.Collections.Generic;" +
        "using System.Linq;" +
        // "using Newtonsoft.Json;" +
        "using Linqin.Api.Models;" +

        "public static class Helper" +
        "{" +
          "public static IEnumerable<ShapeModel> ExecuteQuery(IEnumerable<ShapeModel> shapes)" +
          "{" +
          $"var res = shapes{linqQuery}" +
          "return res;" +
          "}" +

        "}";


      // Detect the file location for the library that defines the object type
      var locations = new List<string>(){
        typeof(object).GetTypeInfo().Assembly.Location,
        typeof(Enumerable).GetTypeInfo().Assembly.Location,
        // var newtonsoftRefLocation = typeof(JsonConvert).GetTypeInfo().Assembly.Location;
        typeof(ShapeModel).GetTypeInfo().Assembly.Location,
        Assembly.Load("System.Runtime, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a").Location,
        Assembly.Load("netstandard, Version=2.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51").Location
      };
      // // Create a reference to the library
      var references = locations.Select(path => MetadataReference.CreateFromFile(path));
      // var systemReference = MetadataReference.CreateFromFile(systemRefLocation);
      // var linqReference = MetadataReference.CreateFromFile(linqRefLocation);
      // // var newtodsoftReference = MetadataReference.CreateFromFile(newtonsoftRefLocation);
      // var runtimeReference = MetadataReference.CreateFromFile(runtimeRefLocation);
      // var netstandardReference = MetadataReference.CreateFromFile(netstandardLocation);
      // var linqinReference = MetadataReference.CreateFromFile(LinqinRefLocation);

      var tree = SyntaxFactory.ParseSyntaxTree(code, CSharpParseOptions.Default.WithKind(SourceCodeKind.Script));
      var compilation = CSharpCompilation.CreateScriptCompilation("Temp")
        .WithOptions(
          new CSharpCompilationOptions(OutputKind.WindowsRuntimeApplication))
            // .AddReferences(new MetadataReference[] { systemReference, linqReference, /*newtodsoftReference,*/ runtimeReference, netstandardReference, linqinReference })
            .AddReferences(references)
            .AddSyntaxTrees(tree);

      using (var dll = new MemoryStream())
      using (var pdb = new MemoryStream())
      {
        EmitResult compilationResult = compilation.Emit(dll, pdb);

        if (compilationResult.Success)
        {
          var assembly = Assembly.Load(dll.ToArray(), pdb.ToArray());

          var type = assembly.GetType("Script+Helper");
          var method = type.GetMethod("ExecuteQuery");
          var result = method.Invoke(null, new object[] { startCollection });

          var str = result as IEnumerable<ShapeModel>;

          return new ResponsePost()
          {
            listOfShapes = str.ToList()
          };
        }
        else
        {
          var issue = "";
          foreach (Diagnostic codeIssue in compilationResult.Diagnostics)
          {
            issue += $"ID: {codeIssue.Id}, Message: {codeIssue.GetMessage()},Location: {codeIssue.Location.GetLineSpan()},Severity: {codeIssue.Severity}";
            Console.WriteLine(issue);
          }

          return new ResponsePost()
          {
            listOfShapes = Array.Empty<ShapeModel>().ToList(),
            ErrorMessage = issue
          };
        }
      }
    }
  }
}