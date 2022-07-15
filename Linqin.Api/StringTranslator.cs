using System.Text;
using System.Linq;

namespace Linqin.Api;

public class StringTranslator
{
  //   private static string CreateExecuteMethodTemplate(string content)
  //   {
  //     var builder = new StringBuilder();

  //     builder.Append("using System;");
  //     builder.Append("\r\nnamespace Lab");
  //     builder.Append("\r\n{");
  //     builder.Append("\r\npublic sealed class Cal");
  //     builder.Append("\r\n{");
  //     builder.Append("\r\npublic static object Execute()");
  //     builder.Append("\r\n{");
  //     builder.AppendFormat("\r\nreturn {0};", content);
  //     builder.Append("\r\n}");
  //     builder.Append("\r\n}");
  //     builder.Append("\r\n}");

  //     return builder.ToString();
  //   }

  //   private static object Execute(string content)
  //   {
  //     var codeProvider = new CSharpCodeProvider(); 
  //     var compilerParameters = new CompilerParameters
  //     {
  //       GenerateExecutable = false,
  //       GenerateInMemory = true
  //     };

  //     compilerParameters.ReferencedAssemblies.Add("system.dll");

  //     string sourceCode = CreateExecuteMethodTemplate(content);
  //     CompilerResults compilerResults = codeProvider.CompileAssemblyFromSource(compilerParameters, sourceCode);
  //     Assembly assembly = compilerResults.CompiledAssembly;
  //     Type type = assembly.GetType("Lab.Cal");
  //     MethodInfo methodInfo = type.GetMethod("Execute");
  //      return methodInfo.Invoke(null, null);
  // //   }
  // private static string CreateExecuteMethodTemplate(string content)
  // {
  //   var builder = new StringBuilder();

  //   builder.Append("using System;");
  //   builder.Append("\r\nnamespace Lab");
  //   builder.Append("\r\n{");
  //   builder.Append("\r\npublic sealed class Cal");
  //   builder.Append("\r\n{");
  //   builder.Append("\r\npublic static object Execute()");
  //   builder.Append("\r\n{");
  //   builder.AppendFormat("\r\nreturn {0};", content);
  //   builder.Append("\r\n}");
  //   builder.Append("\r\n}");
  //   builder.Append("\r\n}");

  //   return builder.ToString();
  // }
  // public static object Execute(string content)
  // {
  //   // New virtual compiler is created to be able to use CompileAssemblyFromSource()
  //   // var codeProvider = new CSharpCodeProvider();
  //   var codeProvider + new Codecompile = new CSharpCodeProvider();

  //   // settings for virtual compiler 
  //   var compilerParameters = new ProviderOptions
  //   {

  //     // GenerateExecutable = false,
  //     // GenerateInMemory = true
  //   };

  //   compilerParameters.ReferencedAssemblies.Add("system.dll");

  //   // Create sourceCode string from user input
  //   string sourceCode = CreateExecuteMethodTemplate(content);
  //   // Compile source code using the virtual compiler settings
  //   CompilerResults compilerResults = codeProvider.CompileAssemblyFromSource(compilerParameters, sourceCode);
  //   // Take the assembly from the compiler results into a new variable
  //   Assembly assembly = compilerResults.CompiledAssembly;
  //   // Get the class which hold the method we want to get a hold of
  //   Type type = assembly.GetType("Lab.Cal");
  //   // Get the method from the class we got in the previous step
  //   MethodInfo methodInfo = type.GetMethod("Execute");
  //   // Executes the method.
  //   return methodInfo.Invoke(null, null);
  // }

  public static List<int> ExecuteQuery()
  {
    var linqMethods = new Dictionary<string, Func<IEnumerable<int>, string, IEnumerable<int>>>();
    linqMethods.Add(".Where", (input, expression) => input.Where(s => IsTrue(s, expression)));
    linqMethods.Add(".OrderBy", (input, expression) => input.OrderBy(s => s));

    var input = ".Where(s => s > 1).Where(s => s < 7)";
    var methods = input.Split(new char[] { '(', ')' });
    var shapes = new List<int> { 1, 4, 5, 7, 2, 9, 2, 6, 1 };

    // var methods = input.Split('.');
    var method = methods[0];
    var exp = string.Join("", methods[1].Skip(5));
    var result = linqMethods[method](shapes.AsEnumerable<int>(), exp);

    for (int i = 2; i < methods.Length; i += 2)
    {
      if (methods[i] != "")
        result = linqMethods[methods[i]](shapes, string.Join("", methods[i + 1].Skip(5)));
      // result = linqMethods[methods[i]](result);
    }

    var test = result.ToList();

    return test;
  }

  private static bool IsTrue(int s, string expression)
  {
    var parts = expression.Split(" ");
    var value = int.Parse(parts[2]);

    if (parts[1] == ">") return s > value;
    if (parts[1] == "<") return s < value;
    if (parts[1] == ">=") return s >= value;
    if (parts[1] == "<=") return s <= value;
    if (parts[1] == "==") return s == value;

    return false;
  }

}