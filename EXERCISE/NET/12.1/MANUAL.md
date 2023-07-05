In Azure Portal:
    Create Azure Resources:
        1- Create a resource group for Azure resources.
            Replace <myLocation> with a region near you.
                """
                    az group create --name az204-redis-rg --location <myLocation>
                """
        
        2- Create an Azure Cache for Redis instance by using the az redis create command.
        The instance name needs to be unique and the script below will attempt to generate one for you, replace <myLocation> with the region you used in the previous step.
            """
                redisName=az204redis$RANDOM
                az redis create --location <myLocation> \
                    --resource-group az204-redis-rg \
                    --name $redisName \
                    --sku Basic --vm-size c0
            """

        3- In the Azure portal, navigate to the new Redis Cache you created.

        4- Select Access keys in the Settings section of the Navigation Pane and leave the portal open. 
        Copy the Primary connection string (StackExchange.Redis) value to use in the app later.

    Create the console application:
        1- Create a console app by running the command below in the Visual Studio Code terminal.

        2- Open the app in Visual Studio Code by selecting File > Open Folder and choosing the folder for the app.

        3- Add the StackExchange.Redis package to the project.
            """
                dotnet add package StackExchange.Redis
            """

        4- Delete any existing code in the Program.cs file and add the following using statement at the top.
            """
                using StackExchange.Redis;
            """
        
        5- Add the following variable after the using statement, replace <REDIS_CONNECTION_STRING> with the Primary connection string (StackExchange.Redis) from the portal.
            """
                // connection string to your Redis Cache    
                string connectionString = "REDIS_CONNECTION_STRING";
            """

        6- Append the following code in the Program.cs file:
            """
                using (var cache = ConnectionMultiplexer.Connect(connectionString))
                    {
                        IDatabase db = cache.GetDatabase();

                        // Snippet below executes a PING to test the server connection
                        var result = await db.ExecuteAsync("ping");
                        Console.WriteLine($"PING = {result.Type} : {result}");

                        // Call StringSetAsync on the IDatabase object to set the key "test:key" to the value "100"
                        bool setValue = await db.StringSetAsync("test:key", "100");
                        Console.WriteLine($"SET: {setValue}");

                        // StringGetAsync retrieves the value for the "test" key
                        string getValue = await db.StringGetAsync("test:key");
                        Console.WriteLine($"GET: {getValue}");
                    }
            """

        7- In the Visual Studio Code terminal, run the commands below to build the app to check for errors, and then run the app using the commands below
            """
                dotnet build
                dotnet run
            """

        The output should be similar to the following:
            """
                PING = SimpleString : PONG
                SET: True
                GET: 100
            """

        8- Return to the portal and select Activity log in the Azure Cache for Redis blade.
        You can view the operations in the log.

    Clean up resources:
        When the resources are no longer needed, you can use the az group delete command to remove the resource group.
            """
                az group delete -n az204-redis-rg --no-wait
            """