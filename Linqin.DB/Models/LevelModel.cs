
namespace Linqin.DB.Models;

public class Level
{
  public int Id { get; set; }
  public string Title { get; set; }
  public string LinqMethod { get; set; }
  public string Description { get; set; }
  public List<GeometryShape> StartCollection { get; set; }
  public List<GeometryShape> ExpectedCollection { get; set; }
}