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
using System.Runtime.InteropServices;

namespace LinqCompiler
{
    public static class Compiler
    {

        public static object? CreateThread(Func<object?> func)
        {
            object? back = null;
            // var fn = new Action(() =>
            // {
            //   using (source.Token.Register(() => thread.Abort()))
            //   {
            //     back = func();
            //   }
            // });
            try
            {
                var thread = new Thread(() =>
                  {

                      back = func();
                  });

                thread.Start();
      
                if (!thread.Join(3000))
                {
                    // I know... I know...
                    Console.WriteLine("Hmm");
                    thread.Suspend();
                    thread.Interrupt();
                    thread.Abort();
                }
                else{
                    Console.WriteLine("Jaha");
                }
            }
            catch (PlatformNotSupportedException abortException)
            {
                // Sue me
                throw new Exception("Query ran for too long.");
            }
            return back;
        }
        // public static async Task<string> CreateThread()
        // {
        //   string back = "";
        //   try
        //   {
        //     var thread = new Thread(() =>
        //     {
        //       back = "hmm";
        //     });

        //     thread.Start();

        //     if (!thread.Join(200))
        //     {
        //       // I know... I know...
        //       thread.Abort();
        //       thread.Join();
        //     }
        //   }
        //   catch (PlatformNotSupportedException abortException)
        //   {
        //     // Sue me
        //     throw new Exception("Query ran for too long.");
        //   }
        //   return back;
        // }

        // public static T RunWithAbort<T>(this Func<T> func, int milliseconds) => RunWithAbort(func, new TimeSpan(0, 0, 0, 0, milliseconds));
        // public static T RunWithAbort<T>(this Func<T> func, TimeSpan delay)
        // {
        //   if (func == null)
        //     throw new ArgumentNullException(nameof(func));

        //   var source = new CancellationTokenSource(delay);
        //   var item = default(T);
        //   var handle = IntPtr.Zero;
        //   var fn = new Action(() =>
        //   {
        //     using (source.Token.Register(() => TerminateThread(handle, 0)))
        //     {
        //       item = func();
        //     }
        //   });

        //   // source.CancelAfter(2000);
        //   handle = CreateThread(IntPtr.Zero, IntPtr.Zero, fn, IntPtr.Zero, 0, out var id);

        //   // source.Cancel();
        //   // var result = WaitForSingleObject(handle, 100 + (int)delay.TotalMilliseconds);
        //   var result = WaitForSingleObject(handle, 100 + (int)delay.Milliseconds);
        //   var rest = TerminateThread(handle, 0);
        //   var err = GetLastError();
        //   // source.Dispose();
        //   CloseHandle(handle);

        //   Console.WriteLine(result);
        //   return item;
        // }

        // [DllImport("kernel32")]
        // private static extern bool TerminateThread(IntPtr hThread, int dwExitCode);

        // [DllImport("kernel32")]
        // private static extern IntPtr CreateThread(IntPtr lpThreadAttributes, IntPtr dwStackSize, Delegate lpStartAddress, IntPtr lpParameter, int dwCreationFlags, out int lpThreadId);

        // [DllImport("kernel32")]
        // private static extern bool CloseHandle(IntPtr hObject);

        // [DllImport("kernel32")]
        // private static extern int WaitForSingleObject(IntPtr hHandle, int dwMilliseconds);
        // [DllImport("kernel32")]
        // private static extern uint GetLastError();

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


                    // var result = RunWithAbort<object?>(() => method?.Invoke(null, new object[] { startCollection }), 5000);
                    var result = CreateThread(() => method?.Invoke(null, new object[] { startCollection }));

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

// using System;
// using System.IO;
// using System.Reflection;
// using System.Runtime.Loader;
// using Microsoft.CodeAnalysis;
// using Microsoft.CodeAnalysis.CSharp;
// using Microsoft.CodeAnalysis.Emit;
// using Linqin.Api.Models;
// using Newtonsoft.Json;
// using System.Linq;
// using System;
// using System.Collections.Concurrent;
// using System.Threading;
// using System.Threading.Tasks;

// namespace LinqCompiler
// {
//   public static class Compiler
//   {

//     public static async Task<ResponsePost> CreateThread()
//     {
//       var thread = new Thread(new ThreadStart());
//     }

//   }
//   internal class CompilerThread
//   {
//     private string _linqQuery = "";
//     private IEnumerable<ShapeModel>  _shapeCollection;


//     public CompilerThread(string linqQuery, IEnumerable<ShapeModel> shapeCollection)
//     {
//       _linqQuery = linqQuery;
//       _shapeCollection = shapeCollection;
//     }

//     public async Task<ResponsePost> CompileAndExecute()
//     {
//       string code = "using System;" +
//         "using System.IO;" +
//         "using System.Collections.Generic;" +
//         "using System.Linq;" +
//         // "using Newtonsoft.Json;" +
//         "using Linqin.Api.Models;" +

//         "public static class Helper" +
//         "{" +

//           "public static object ExecuteQuery(IEnumerable<ShapeModel> shapes)" +
//           "{" +
//             "try" +
//             "{" +
//               $"return shapes.{_linqQuery}" +
//             "}" +
//             "catch(Exception ex)" +
//             "{" +
//               "return ex.Message;" +
//             "}" +
//           "}" +

//         "}";


//       // Detect the file location for the library that defines the object type
//       var locations = new List<string>(){
//         typeof(object).GetTypeInfo().Assembly.Location,
//         typeof(Enumerable).GetTypeInfo().Assembly.Location,
//         // var newtonsoftRefLocation = typeof(JsonConvert).GetTypeInfo().Assembly.Location;
//         typeof(ShapeModel).GetTypeInfo().Assembly.Location,
//         Assembly.Load("System.Runtime, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a").Location,
//         Assembly.Load("netstandard, Version=2.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51").Location
//       };

//       // // Create a reference to the library
//       var references = locations.Select(path => MetadataReference.CreateFromFile(path));

//       var tree = SyntaxFactory.ParseSyntaxTree(code, CSharpParseOptions.Default.WithKind(SourceCodeKind.Script));
//       var compilation = CSharpCompilation.CreateScriptCompilation("Temp")
//         .WithOptions(
//           new CSharpCompilationOptions(OutputKind.WindowsRuntimeApplication))
//             .AddReferences(references)
//             .AddSyntaxTrees(tree);

//       using (var dll = new MemoryStream())
//       using (var pdb = new MemoryStream())
//       {
//         EmitResult compilationResult = compilation.Emit(dll, pdb);

//         if (compilationResult.Success)
//         {
//           var assembly = Assembly.Load(dll.ToArray(), pdb.ToArray());

//           var type = assembly.GetType("Script+Helper");
//           var method = type?.GetMethod("ExecuteQuery");

//           var result = method?.Invoke(null, new object[] { _shapeCollection });

//           // var str = result as IEnumerable<ShapeModel>;

//           // if (str == null)
//           //   throw new Exception("Unknown error at execution");

//           return new ResponsePost() { ListOfShapes = result };
//         }
//         else
//         {
//           var issue = "";
//           foreach (Diagnostic codeIssue in compilationResult.Diagnostics)
//           {
//             // issue += $"ID: {codeIssue.Id}, Message: {codeIssue.GetMessage()},Location: {codeIssue.Location.GetLineSpan()},Severity: {codeIssue.Severity}";
//             issue += $"{codeIssue.GetMessage()}";
//           }

//           return new ResponsePost()
//           {
//             ListOfShapes = new List<ShapeModel>(),
//             ErrorMessage = issue
//           };
//         }
//       }
//     }
//   }
// }