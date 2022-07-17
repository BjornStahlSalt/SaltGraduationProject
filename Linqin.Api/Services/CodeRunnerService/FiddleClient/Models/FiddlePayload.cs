namespace Fiddle.Models;

public class MvcCodeBlock
{
  public string Model { get; set; } = "";
  public string View { get; set; } = "";
  public string Controller { get; set; } = "";
}

public class OriginalMvcCodeBlock
{
  public string Model { get; set; } = "";
  public string View { get; set; } = "";
  public string Controller { get; set; } = "";
}

public class FiddlePayload
{
  public FiddlePayload(string code)
  {
    CodeBlock = code;
  }
  public string CodeBlock { get; set; }
  public string OriginalCodeBlock { get; set; } = "";
  public string Language { get; set; } = "CSharp";
  public string Compiler { get; set; } = "Net45";
  public string ProjectType { get; set; } = "Console";
  public string OriginalFiddleId { get; set; } = "CsCons";
  public string NuGetPackageVersionIds { get; set; } = "";
  public string OriginalNuGetPackageVersionIds { get; set; } = "";
  public string TimeOffset { get; set; } = "2";
  public List<object> ConsoleInputLines { get; set; } = new List<object>();
  public string MvcViewEngine { get; set; } = "Razor";
  public MvcCodeBlock MvcCodeBlock { get; set; } = new MvcCodeBlock();
  public OriginalMvcCodeBlock OriginalMvcCodeBlock { get; set; } = new OriginalMvcCodeBlock();
  public bool UseResultCache { get; set; } = false;
}
