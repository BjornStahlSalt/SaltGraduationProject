using Azure.Data.Tables;
using Azure;

namespace Linqin.DB.Models;

public class Level : ITableEntity
{
  public string Id { get; set; }
  public string? PartitionKey { get; set; }
  public string? RowKey { get; set; }
  public DateTimeOffset? Timestamp { get; set; }
  public ETag ETag { get; set; }

  public string Title { get; set; }
  public string LinqMethod { get; set; }
  public string Description { get; set; }
  public string StartCollection { get; set; }
  public string ExpectedCollection { get; set; }
}