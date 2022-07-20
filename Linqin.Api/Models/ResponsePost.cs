namespace Linqin.Api.Models;

public class ResponsePost
{
  public List<ShapeModel> listOfShapes { get; set; }
  public string ErrorMessage { get; set; }
}