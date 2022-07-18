using Fiddle;
using Fiddle.Exceptions;

namespace CodeRunner;

public class CodeRunnerService
{
    private readonly FiddleClient _fiddleClient = new FiddleClient();
    private string FormatPayload(string listDef, string linqQuery) =>
      "using System;" +
      "using System.Linq;" +
      "public class Program {" +
      "public static void Main() {" +
      $"{listDef}" +
      $"var res = {linqQuery}" +
      "Console.WriteLine(String.Join(&quot;, &quot;, res));}}";

    public async Task<string> RunLinqQueryOnList(string listDef, string query)
    {
        var code = FormatPayload(listDef, query);
        try
        {
            var fiddleResponse = await _fiddleClient.Run(code);
            if (fiddleResponse.ConsoleOutput.Contains("error"))
            {
                return fiddleResponse.ConsoleOutput.Split(':').Last();
            }
            return fiddleResponse.ConsoleOutput;
        }
        catch (FiddleClientError)
        {
            return "Uh oh! Something went wrong.";
        }
    }
}


