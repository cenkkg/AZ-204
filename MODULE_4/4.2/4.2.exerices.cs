// 1) Before using C# part, firstly, you need to login Azure. You can use below commands in order.
// az login -> login to Azure Account

// If you do not have  resouce group you need to run below.
// az group create --location <myLocation> --name az204-cosmos-rg

// Create the Azure Cosmos DB account.
// az cosmosdb create --name <myCosmosDBacct> --resource-group az204-cosmos-rg

// Retrieve the primary key
// az cosmosdb keys list --name <myCosmosDBacct> --resource-group az204-cosmos-rg

// 2) You need to add Microsoft.Azure.Cosmos, if it is not available in your device.
// dotnet add package Microsoft.Azure.Cosmos


using Microsoft.Azure.Cosmos;

public class Program
{
    // Replace <documentEndpoint> with the information created earlier
    private static readonly string EndpointUri = "<documentEndpoint>";

    // Set variable to the Primary Key from earlier.
    private static readonly string PrimaryKey = "<your primary key>";

    // The Cosmos client instance
    private CosmosClient cosmosClient;

    // The database we will create
    private Database database;

    // The container we will create.
    private Container container;

    // The names of the database and container we will create
    private string databaseId = "az204Database";
    private string containerId = "az204Container";

    public static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine("Beginning operations...\n");
            Program p = new Program();
            await p.CosmosAsync();

        }
        catch (CosmosException de)
        {
            Exception baseException = de.GetBaseException();
            Console.WriteLine("{0} error occurred: {1}", de.StatusCode, de);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: {0}", e);
        }
        finally
        {
            Console.WriteLine("End of program, press any key to exit.");
            Console.ReadKey();
        }
    }
    //The sample code below gets added below this line

    public async Task CosmosAsync()
    {
        // Create a new instance of the Cosmos Client
        this.cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);

        // Runs the CreateDatabaseAsync method
        await this.CreateDatabaseAsync();

        // Run the CreateContainerAsync method
        await this.CreateContainerAsync();
    }

    private async Task CreateDatabaseAsync()
    {
        // Create a new database using the cosmosClient
        this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
        Console.WriteLine("Created Database: {0}\n", this.database.Id);
    }

    private async Task CreateContainerAsync()
    {
        // Create a new container
        this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, "/LastName");
        Console.WriteLine("Created Container: {0}\n", this.container.Id);
    }
}

// You need to build application first, you can use this:
// dotnet build

// Afterwards, you can run application with:
// dotnet run

// Lastly, you can delete resources with (Deleting Resource Group):
// az group delete --name az204-cosmos-rg --no-wait