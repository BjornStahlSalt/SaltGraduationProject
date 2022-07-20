using Azure.Data.Tables;
using Linqin.DB.Models;
using Azure;
using Newtonsoft.Json;

namespace Linqin.DB.Data;

public class LevelRepository
{
  private TableClient _tableClient;
  private string _tableName = "Levels";
  private string _partitionKey = "PartitionKey";
  public LevelRepository()
  {
    CreateTable();
    _tableClient = ConnectToTable();
  }
  public void CreateTable()
  {
    var storageUri = Environment.GetEnvironmentVariable("AZURE_TABLE_STORAGE_URI");
    var accountName = Environment.GetEnvironmentVariable("AZURE_TABLE_STORAGE_ACCOUNT_NAME");
    var storageAccountKey = Environment.GetEnvironmentVariable("AZURE_TABLE_STORAGE_ACCOUNT_KEY");

    var serviceClient = new TableServiceClient(new Uri(storageUri), new TableSharedKeyCredential(accountName, storageAccountKey));
    serviceClient.CreateTableIfNotExists(_tableName);
    // Console.WriteLine($"The created table's name is {table.Name}.");
  }

  public TableClient ConnectToTable()
  {
    var connectionString = Environment.GetEnvironmentVariable("AZURE_TABLE_STORAGE_CONNECTIONSTRING");
    TableServiceClient serviceClient = new TableServiceClient(connectionString);
    return new TableClient(connectionString, _tableName);
  }


  public string AddData(PostRequest request)
  {
    var rowKey = Guid.NewGuid().ToString();
    var entity = new TableEntity(_partitionKey, rowKey) {
      { "Id", rowKey },
      { "Title", request.Title },
      { "LinqMethod", request.LinqMethod },
      { "Description", request.Description },
      { "StartCollection", JsonConvert.SerializeObject(request.StartCollection) },
      { "ExpectedCollection", JsonConvert.SerializeObject(request.ExpectedCollection) }
      };
    _tableClient.AddEntity(entity);
    return rowKey;
  }

  public List<GetResponse> GetAllData()
  {
    var levels = _tableClient.Query<Level>();
    var listOfLevels = new List<GetResponse>();

    foreach (var level in levels)
    {
      listOfLevels.Add(
        new GetResponse()
        {
          Id = level.Id,
          Title = level.Title,
          Description = level.Description,
          LinqMethod = level.LinqMethod,
          StartCollection = JsonConvert.DeserializeObject<List<GeometryShape>>(level.StartCollection),
          ExpectedCollection = JsonConvert.DeserializeObject<List<GeometryShape>>(level.ExpectedCollection)
        }
      );
    }
    return listOfLevels;
  }

  public GetResponse GetData(string id)
  {
    // dynamic entities ()
    //Pageable<Level> queryResultsFilter = TableClient.Query(filter: $"PartitionKey eq '{partitionKey}'");

    // typed entities 
    Pageable<Level> queryResultsLINQ = _tableClient.Query<Level>(lev => lev.Id == id);

    var level = queryResultsLINQ.First();
    return new GetResponse()
    {
      Id = level.Id,
      Title = level.Title,
      Description = level.Description,
      LinqMethod = level.LinqMethod,
      StartCollection = JsonConvert.DeserializeObject<List<GeometryShape>>(level.StartCollection),
      ExpectedCollection = JsonConvert.DeserializeObject<List<GeometryShape>>(level.ExpectedCollection)
    };
    // return new Level()
    // {
    //   id = id,
    //   Title = 
    // }
  }
  public void DeleteData(string id)
  {
    _tableClient.DeleteEntity(_partitionKey, id);
  }

}