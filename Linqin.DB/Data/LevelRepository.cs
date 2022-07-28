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

    var serviceClient = new TableServiceClient(
      new Uri(storageUri),
      new TableSharedKeyCredential(accountName, storageAccountKey)
    );

    serviceClient.CreateTableIfNotExists(_tableName);
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
      { "LevelDifficulty", request.LevelDifficulty },
      { "Prompt", request.Prompt },
      { "Description", request.Description },
      { "StartCollection", JsonConvert.SerializeObject(request.StartCollection) },
      { "ExpectedBool", JsonConvert.SerializeObject(request.ExpectedBool) },
      { "ExpectedInt", JsonConvert.SerializeObject(request.ExpectedInt) },
      { "ExpectedCollection", JsonConvert.SerializeObject(request.ExpectedCollection) },
      { "ExpectedSingle", JsonConvert.SerializeObject(request.ExpectedSingle) },
      { "ExpectedString", JsonConvert.SerializeObject(request.ExpectedString) }
      };
    _tableClient.AddEntity(entity);
    return rowKey;
  }

  public void UpdateData(string id, PostRequest request)
  {
    var level = GetOne(id);
    level.Title = JsonConvert.SerializeObject(request.Title);
    level.Prompt = JsonConvert.SerializeObject(request.Prompt);
    level.LevelDifficulty = JsonConvert.SerializeObject(request.LevelDifficulty);
    level.Description = JsonConvert.SerializeObject(request.Description);
    level.StartCollection = JsonConvert.SerializeObject(request.StartCollection);
    level.ExpectedBool = JsonConvert.SerializeObject(request.ExpectedBool);
    level.ExpectedInt = JsonConvert.SerializeObject(request.ExpectedInt);
    level.ExpectedCollection = JsonConvert.SerializeObject(request.ExpectedCollection);
    level.ExpectedSingle = JsonConvert.SerializeObject(request.ExpectedSingle);
    level.ExpectedString = JsonConvert.SerializeObject(request.ExpectedString);
    _tableClient.UpdateEntity(level, ETag.All, TableUpdateMode.Merge);
  }

  public List<Level> GetAll()
    => _tableClient.Query<Level>().ToList();

  public Level GetOne(string id)
    => _tableClient.Query<Level>(lev => lev.Id == id).First();

  public void Delete(string id)
    => _tableClient.DeleteEntity(_partitionKey, id);

  public GetResponse DeserializeLevel(Level level) => new GetResponse()
  {
    Id = level.Id,
    Title = level.Title,
    LevelDifficulty = level.LevelDifficulty,
    Prompt = level.Prompt,
    Description = level.Description,
    StartCollection = JsonConvert.DeserializeObject<List<ShapeModel>>(level.StartCollection),
    ExpectedBool = JsonConvert.DeserializeObject<bool?>(level.ExpectedBool),
    ExpectedInt = JsonConvert.DeserializeObject<int?>(level.ExpectedInt),
    ExpectedCollection = JsonConvert.DeserializeObject<List<ShapeModel>?>(level.ExpectedCollection),
    ExpectedSingle = JsonConvert.DeserializeObject<ShapeModel?>(level.ExpectedSingle),
    ExpectedString = JsonConvert.DeserializeObject<List<string>?>(level.ExpectedString),
  };
}