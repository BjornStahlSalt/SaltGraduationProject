using Newtonsoft.Json;
using Fiddle;
using Fiddle.Exceptions;
using Linqin.Api.Models;

namespace CodeRunner;

public class CodeRunnerService
{
  private readonly FiddleClient _fiddleClient = new FiddleClient();

  public string GetListAsString(List<ShapeModel> listOfShapes)
  {
    var stringOfShapes = "var shapes = new List<object>(){";

    foreach (var shape in listOfShapes)
    {
      stringOfShapes += ($"new {{Shape = \"{shape.Shape}\", Color = \"{shape.Color}\", PriorityValue = {shape.PriorityValue}}}");
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
    "using Newtonsoft.Json;" +
    "using System.Collections.Generic;" +
    "public class Program {" +
    "public static void Main() {" +
    $"{listDef}" +
    $"var res = {linqQuery}" +
    "Console.WriteLine(JsonConvert.SerializeObject(res));" +
    "}}";
  // string test = JsonConvert.SerializeObject(listOfShapes);
  // "Console.WriteLine(String.Join(&quot;, &quot;, res));" +
  public async Task<List<ShapeModel>> RunLinqQueryOnList(List<ShapeModel> listOfShapes, string query)
  {
    var listDef = GetListAsString(listOfShapes);
    var code = FormatPayload(listDef, query);
    Console.WriteLine(code);
    // try
    // {
    var fiddleResponse = await _fiddleClient.Run(code);
    if (fiddleResponse.ConsoleOutput.Contains("error"))
    {
      throw new FiddleClientError(fiddleResponse.ConsoleOutput.Split(':').Last());
    }
    return JsonConvert.DeserializeObject<List<ShapeModel>>(fiddleResponse.ConsoleOutput);
  }
  // catch (FiddleClientError ex)
  // {
  //   // return ex.Message;
  //   return null;
  // }
  // }
}


