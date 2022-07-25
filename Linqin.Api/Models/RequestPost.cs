namespace Linqin.Api.Models;

public class RequestPost
{
  public List<ShapeModel> listOfShapes { get; set; } = new List<ShapeModel>();
  public string Query { get; set; } = "";
}