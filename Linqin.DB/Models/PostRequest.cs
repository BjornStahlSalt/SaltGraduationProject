using Azure.Data.Tables;
using Azure;

namespace Linqin.DB.Models;

public class PostRequest
{
  public string? Title { get; set; }
  public string? LevelDifficulty { get; set; }
  public string? Prompt { get; set; }
  public string? Description { get; set; }
  public List<ShapeModel>? StartCollection { get; set; }
  public bool? ExpectedBool { get; set; } = null; 
  public int? ExpectedInt { get; set; } = null; 
  public List<ShapeModel>? ExpectedCollection { get; set; } = null; 
  public ShapeModel? ExpectedSingle { get; set; } = null; 
}