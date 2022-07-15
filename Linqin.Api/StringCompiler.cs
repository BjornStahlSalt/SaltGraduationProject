using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace RoslynCore
{
  public static class EmitDemo
  {
    public static void ExecuteString(string linqQuery, IEnumerable<int> startCollection)
    {
      string code = @"using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
namespace RoslynCore
{
  public static class Helper
  {
    public static IEnumerable<int> ExecuteQuery(IEnumerable<int> shapes)
    {"
     + $"return shapes{linqQuery};"
  + @"}
  }
}";
      var tree = SyntaxFactory.ParseSyntaxTree(code);
      string fileName = "mylib.dll";
      // Detect the file location for the library that defines the object type
      var systemRefLocation = typeof(object).GetTypeInfo().Assembly.Location;
      var linqRefLocation = typeof(Enumerable).GetTypeInfo().Assembly.Location;
      var IEnumerableRefLocation = typeof(IEnumerable<>).GetTypeInfo().Assembly.Location;
      // Create a reference to the library
      var systemReference = MetadataReference.CreateFromFile(systemRefLocation);
      var linqReference = MetadataReference.CreateFromFile(linqRefLocation);
      var IEnumerableReference = MetadataReference.CreateFromFile(IEnumerableRefLocation);

      var references = AppDomain.CurrentDomain
              .GetAssemblies()
              .Where(a => !a.IsDynamic)
              .Select(a => a.Location)
              .Where(s => !string.IsNullOrEmpty(s))
              .Where(s => !s.Contains("xunit"))
              .Select(s => MetadataReference.CreateFromFile(s))
              .ToArray();

      // A single, immutable invocation to the compiler
      // to produce a library
      var compilation = CSharpCompilation.Create(fileName)
        .WithOptions(
          new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
            // .AddReferences(new MetadataReference[] { systemReference, linqReference, IEnumerableReference })
            .AddReferences(references)
            .AddSyntaxTrees(tree);
      string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
      EmitResult compilationResult = compilation.Emit(path);
      if (compilationResult.Success)
      {
        // Load the assembly
        Assembly asm = AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
        // Invoke the RoslynCore.Helper.CalculateCircleArea method passing an argument
        // double radius = 10;
        var result = asm.GetType("RoslynCore.Helper").GetMethod("ExecuteQuery").Invoke(null, new object[] { startCollection });
        foreach (var num in result as IEnumerable<int>)
        {
          Console.WriteLine(num);
        }
        // Console.WriteLine($"Circle area with radius = {radius} is {result}");
        Console.WriteLine($"***************************************************************************");
      }
      else
      {
        foreach (Diagnostic codeIssue in compilationResult.Diagnostics)
        {
          string issue = $"ID: {codeIssue.Id}, Message: {codeIssue.GetMessage()},Location: {codeIssue.Location.GetLineSpan()},Severity: {codeIssue.Severity}";
          Console.WriteLine(issue);
        }
      }
    }
  }
}