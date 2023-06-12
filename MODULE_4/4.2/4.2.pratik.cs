using System;

class Program
{
    static void Main(string[] args)
    {
        CosmosClient client = new CosmosClient(endpoint, key); // Creating "CosmosClient", CosmosClient is thread-safe!
        DatabaseResponse databaseResponse = await client.CreateDatabaseIfNotExistsAsync(databaseId, 10000); // Create new DB
        
        // DATABASE OPERATIONS ------------------------------------------------------------------------
        DatabaseResponse readResponse = await database.ReadAsync(); // Read DB
        await database.DeleteAsync(); // Delete DB

        // CONTAINER OPERATIONS ------------------------------------------------------------------------
        // Set throughput to the minimum value of 400 RU/s
        ContainerResponse simpleContainer = await database.CreateContainerIfNotExistsAsync(
            id: containerId,
            partitionKeyPath: partitionKey, // So important, it is used for partitoning data in container to different physical partitions.
            throughput: 400);

        Container container = database.GetContainer(containerId);
        ContainerProperties containerProperties = await container.ReadContainerAsync();
    
        await database.GetContainer(containerId).DeleteContainerAsync(); // Delete container

        // ITEM OPERATIONS ------------------------------------------------------------------------
        // Create ITEM
        ItemResponse<SalesOrder> response = await container.CreateItemAsync(salesOrder, new PartitionKey(salesOrder.AccountNumber));

        // READ ITEM
        string id = "[id]";
        string accountNumber = "[partition-key]";
        ItemResponse<SalesOrder> response = await container.ReadItemAsync(id, new PartitionKey(accountNumber));

        // Query ITEM
        QueryDefinition query = new QueryDefinition(
            "select * from sales s where s.AccountNumber = @AccountInput ")
            .WithParameter("@AccountInput", "Account1");

        FeedIterator<SalesOrder> resultSet = container.GetItemQueryIterator<SalesOrder>(
            query,
            requestOptions: new QueryRequestOptions()
            {
                PartitionKey = new PartitionKey("Account1"),
                MaxItemCount = 1
            });
    }
}

