// The following code instantiates a public client application, signing-in users in the Microsoft Azure public cloud, 
//  with their work and school accounts, or their personal Microsoft accounts.
IPublicClientApplication app = PublicClientApplicationBuilder.Create(clientId).Build();

// In the same way, the following code instantiates a confidential application (a Web app located at https://myapp.azurewebsites.net)
string redirectUri = "https://myapp.azurewebsites.net";
IConfidentialClientApplication app = ConfidentialClientApplicationBuilder.Create(clientId)
    .WithClientSecret(clientSecret)
    .WithRedirectUri(redirectUri )
    .Build();

// Builder modifiers
// In the code snippets using application builders, .With methods can be applied as modifiers
// for example, .WithAuthority and .WithRedirectUri

// The .WithAuthority modifier sets the application default authority to an Azure Active Directory authority
var clientApp = PublicClientApplicationBuilder.Create(client_id)
    .WithAuthority(AzureCloudInstance.AzurePublic, tenant_id)
    .Build();

// The .WithRedirectUri modifier overrides the default redirect URI.
var clientApp = PublicClientApplicationBuilder.Create(client_id)
    .WithAuthority(AzureCloudInstance.AzurePublic, tenant_id)
    .WithRedirectUri("http://localhost")
    .Build();

// After APP registration is done...
using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace az204_auth
{
    class Program
    {
        private const string _clientId = "APPLICATION_CLIENT_ID";
        private const string _tenantId = "DIRECTORY_TENANT_ID";

        public static async Task Main(string[] args)
        {
            var app = PublicClientApplicationBuilder
                .Create(_clientId)
                .WithAuthority(AzureCloudInstance.AzurePublic, _tenantId)
                .WithRedirectUri("http://localhost")
                .Build(); 
            string[] scopes = { "user.read" };
            AuthenticationResult result = await app.AcquireTokenInteractive(scopes).ExecuteAsync();

            Console.WriteLine($"Token:\t{result.AccessToken}");
        }
    }
}