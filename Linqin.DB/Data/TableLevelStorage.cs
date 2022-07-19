using Azure.Data.Tables;
using Linqin.DB.Models;

namespace Linqin.DB.Data;

public class TableLevelStorage
{
    private TableClient TableClient { get; set; }
    public TableLevelStorage()
    {
        CreateTable();
        TableClient = ConnectToTable();
    }
    public void CreateTable()
    {
        string tableName = "Levels";
        var storageUri = Environment.GetEnvironmentVariable("AZURE_TABLE_STORAGE_URI");
        var accountName = Environment.GetEnvironmentVariable("AZURE_TABLE_STORAGE_ACCOUNT_NAME");
        var storageAccountKey = Environment.GetEnvironmentVariable("AZURE_TABLE_STORAGE_ACCOUNT_KEY");

        var serviceClient = new TableServiceClient(new Uri(storageUri), new TableSharedKeyCredential(accountName, storageAccountKey));
        serviceClient.CreateTableIfNotExists(tableName);
        // Console.WriteLine($"The created table's name is {table.Name}.");
    }

    public TableClient ConnectToTable()
    {
        var connectionString = Environment.GetEnvironmentVariable("AZURE_TABLE_STORAGE_CONNECTIONSTRING");
        TableServiceClient serviceClient = new TableServiceClient(connectionString);
        return new TableClient(connectionString, "CreatedWithCodeTable");
    }

    public void AddData(TableClient client, Level level)
    {
        var partitionKey = "PartitionKey";
        var rowKey = Guid.NewGuid().ToString();
        var entity = new TableEntity(partitionKey, rowKey) {
      { "Id", level.Id },
      { "Title", level.Title },
      { "Linq Method", level.LinqMethod },
      { "Description", level.Description },
      { "Start Collection", level.StartCollection },
      { "Expected Collection", level.ExpectedCollection }
      };
        client.AddEntity(entity);
    }

    public Level GetData(int id)
    {
        // dynamic entities ()
        Pageable queryResultsFilter = tableClient.Query(filter: $"PartitionKey eq '{partitionKey}'");
        // typed entities 
        Pageable queryResultsLINQ = tableClient.Query<Level>(lev => lev.Id == id);

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