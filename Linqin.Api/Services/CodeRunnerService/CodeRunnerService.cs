using Fiddle;
using Fiddle.Exceptions;
using Linqin.Api.Models;

namespace CodeRunner;

public class CodeRunnerService
{
  private readonly FiddleClient _fiddleClient = new FiddleClient();

  public string GetListAsString(List<GeometryShapes> listOfShapes)
  {
    var stringOfShapes = "new []{";

    foreach (var shape in listOfShapes)
    {
      stringOfShapes += ($"new {{Shape = {shape.Shape}, Color = {shape.Color}, PriorityValue = {shape.PriorityValue}}}");
      if (listOfShapes.IndexOf(shape) < listOfShapes.Count - 1)
      {
        stringOfShapes += ", ";
      }
    }

    stringOfShapes += " };";
    return stringOfShapes;
  }

  private string FormatPayload(string listDef, string linqQuery) => 
    "using System;" +
    "using System.Linq;" +
    "using System.Collections.Generic;" +
    "public class Program {" +
    "public static void Main() {" +
    $"{listDef}" +
    $"var res = {linqQuery}" +
    "Console.WriteLine(String.Join(&quot;, &quot;, res));" +
    "}}";

  public async Task<string> RunLinqQueryOnList(List<GeometryShapes> listOfShapes, string query)
  {
    var listDef = GetListAsString(listOfShapes);
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
    catch (FiddleClientError ex)
    {
      return ex.Message;
    }
  }
}


