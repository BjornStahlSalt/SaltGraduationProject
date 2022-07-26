namespace Linqin.Api.Models;

public class ResponsePost
{
  public List<ShapeModel> ListOfShapes { get; set; }
  public string ErrorMessage { get; set; }
}