using Library;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true)
    .AddUserSecrets<Program>();

var build =  configBuilder.Build();
var services = new ServiceCollection()
    .Configure<ApexConfig>(build)
    .AddSingleton<IApexClient, LocalApexClient>()
    .BuildServiceProvider();

var apexClient = services.GetRequiredService<IApexClient>();

await apexClient.LoginAsync();
var status = await apexClient.GetStatus();

var ph = status.Inputs.SingleOrDefault(input => input.Did.Equals("base_pH"));
var temp = status.Inputs.SingleOrDefault(input => input.Did.Equals("base_Temp"));

var ato = status.Outputs.SingleOrDefault(output => output.Name.Equals("ATO"));
var atoHi = status.Inputs.SingleOrDefault(input => input.Name.Equals("ATO_Hi"));
var atoLo = status.Inputs.SingleOrDefault(input => input.Name.Equals("ATO_Lo"));

Console.WriteLine($"System: {status.System.Hostname}");
Console.WriteLine($"  PH: {ph?.Value}");
Console.WriteLine($"  Temp: {temp?.Value}");
Console.WriteLine($"  ATO: {ato.GetStatus()} \n\t ATO Low: {atoLo.GetWaterLevel()}\t ATO High: {atoHi.GetWaterLevel()}\n");