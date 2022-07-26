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
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace LinqCompiler
{
  public static class Compiler
  {

    public static async Task<ResponsePost> ExecuteString(string linqQuery, IEnumerable<ShapeModel> startCollection)
    {
      string code = "using System;" +
        "using System.IO;" +
        "using System.Collections.Generic;" +
        "using System.Linq;" +
        // "using Newtonsoft.Json;" +
        "using Linqin.Api.Models;" +

        "public static class Helper" +
        "{" +

          "public static object ExecuteQuery(IEnumerable<ShapeModel> shapes)" +
          "{" +
            "try" +
            "{" +
              $"return shapes.{linqQuery}" +
            "}" +
            "catch(Exception ex)" +
            "{" +
              "return ex.Message;" +
            "}" +
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

      var tree = SyntaxFactory.ParseSyntaxTree(code, CSharpParseOptions.Default.WithKind(SourceCodeKind.Script));
      var compilation = CSharpCompilation.CreateScriptCompilation("Temp")
        .WithOptions(
          new CSharpCompilationOptions(OutputKind.WindowsRuntimeApplication))
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
          var method = type?.GetMethod("ExecuteQuery");

          var result = method?.Invoke(null, new object[] { startCollection });

          // var str = result as IEnumerable<ShapeModel>;

          // if (str == null)
          //   throw new Exception("Unknown error at execution");

          return new ResponsePost() { ListOfShapes = result };
        }
        else
        {
          var issue = "";
          foreach (Diagnostic codeIssue in compilationResult.Diagnostics)
          {
            // issue += $"ID: {codeIssue.Id}, Message: {codeIssue.GetMessage()},Location: {codeIssue.Location.GetLineSpan()},Severity: {codeIssue.Severity}";
            issue += $"{codeIssue.GetMessage()}";
          }

          return new ResponsePost()
          {
            ListOfShapes = new List<ShapeModel>(),
            ErrorMessage = issue
          };
        }
      }
    }
  }
}