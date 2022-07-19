using Azure.Data.Tables;
using Linqin.DB.Models;
using Azure;
using Newtonsoft.Json;

namespace Linqin.DB.Data;

public class TableLevelStorage
{
  private TableClient TableClient { get; set; }
  private string TableName { get; } = "Levels";
  public TableLevelStorage()
  {
    CreateTable();
    TableClient = ConnectToTable();
  }
  public void CreateTable()
  {
    var storageUri = Environment.GetEnvironmentVariable("AZURE_TABLE_STORAGE_URI");
    var accountName = Environment.GetEnvironmentVariable("AZURE_TABLE_STORAGE_ACCOUNT_NAME");
    var storageAccountKey = Environment.GetEnvironmentVariable("AZURE_TABLE_STORAGE_ACCOUNT_KEY");

    var serviceClient = new TableServiceClient(new Uri(storageUri), new TableSharedKeyCredential(accountName, storageAccountKey));
    serviceClient.CreateTableIfNotExists(TableName);
    // Console.WriteLine($"The created table's name is {table.Name}.");
  }

  public TableClient ConnectToTable()
  {
    var connectionString = Environment.GetEnvironmentVariable("AZURE_TABLE_STORAGE_CONNECTIONSTRING");
    TableServiceClient serviceClient = new TableServiceClient(connectionString);
    return new TableClient(connectionString, TableName);
  }


  public void AddData(PostRequest request)
  {
    var partitionKey = "PartitionKey";
    var rowKey = Guid.NewGuid().ToString();
    var entity = new TableEntity(partitionKey, rowKey) {
      { "Id", Guid.NewGuid().ToString() },
      { "Title", request.Title },
      { "LinqMethod", request.LinqMethod },
      { "Description", request.Description },
      { "StartCollection", JsonConvert.SerializeObject(request.StartCollection) },
      { "ExpectedCollection", JsonConvert.SerializeObject(request.ExpectedCollection) }
      };
    TableClient.AddEntity(entity);
  }

  public Level GetData(string id)
  {
    // dynamic entities ()
    //Pageable<Level> queryResultsFilter = TableClient.Query(filter: $"PartitionKey eq '{partitionKey}'");
    // typed entities 
    Pageable<Level> queryResultsLINQ = TableClient.Query<Level>(lev => lev.Id == id);

    return queryResultsLINQ.First();
    // return new Level()
    // {
    //   id = id,
    //   Title = 
    // }
  }
  // delete on specific entity
  // public void DeleteData(int id) 
  // { 
  // await tableClient.DeleteEntityAsync(partitionKey, lastRowKey);
  // }

}