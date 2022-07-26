using Azure.Data.Tables;
using Azure;

namespace Linqin.DB.Models;

public class GetResponse
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string LinqMethod { get; set; }
    public string Description { get; set; }
    public List<GeometryShape> StartCollection { get; set; }
    public List<GeometryShape> ExpectedCollection { get; set; }
}