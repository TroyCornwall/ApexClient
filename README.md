# C# ApexClient

## To use with Fusion (cloud): 

Register the ApexConfig options, and the FusionApexClient
````
services
    .Configure<ApexConfig>(Configuration)
    .AddOptions()
    .AddSingleton<IApexClient, FusionApexClient>()
````

Then set the following secrets
Open a terminal to the root of your project, then run the following commands:


The username and password are what you login to fusion with, the apex client id is used in the requests, easiest to find in the chrome networking tab.
````
dotnet user-secrets set "Username" "<username>"
dotnet user-secrets set "Password" "<password>"
dotnet user-secrets set "ApexClientId" "<apex client id>"
````


## To use with a local apex unit:

Register the ApexConfig options, and the FusionApexClient
````
services
    .Configure<ApexConfig>(Configuration)
    .AddOptions()
    .AddSingleton<IApexClient, LocalApexClient>()
````

Then set the following secrets
Open a terminal to the root of your project, then run the following commands:

The ip address can be found in the network tab of the Apex settings.

The username and password are what you login to the local apex web panel with.

The default username and password are in the example bellow...
````
dotnet user-secrets set "Username" "admin"
dotnet user-secrets set "Password" "1234"
dotnet user-secrets set "IpAddress" "<apex ip address>"
````