using Library;
using Library.Extensions;
using Library.Models.Api.Fusion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true)
    .AddUserSecrets<Program>();


var build =  configBuilder.Build();
var services = new ServiceCollection()
    .Configure<ApexConfig>(build)
    .AddOptions()
    .AddSingleton<IApexClient, FusionApexClient>()
    .BuildServiceProvider();

var apexClient = services.GetRequiredService<IApexClient>();

await apexClient.LoginAsync();
var status = await apexClient.GetStatus();

var ph = status.Inputs.SingleOrDefault(input => input.Did.Equals("base_pH"));
var temp = status.Inputs.SingleOrDefault(input => input.Did.Equals("base_Temp"));

var ato = status.Outputs.SingleOrDefault(output => output.Name.Equals("ATO"));
var atoHi = status.Inputs.SingleOrDefault(input => input.Name.Equals("ATO_Hi"));
var atoLo = status.Inputs.SingleOrDefault(input => input.Name.Equals("ATO_Lo"));

var mLog = await apexClient.GetTestResults();

var alk = mLog.Where(x => x.GetTestType() == TestResult.TestType.Alk).OrderByDescending(x => x.Date).First();
var calc = mLog.Where(x => x.GetTestType() == TestResult.TestType.Calcium).OrderByDescending(x => x.Date).First();
var mg = mLog.Where(x => x.GetTestType() == TestResult.TestType.Magnesium).OrderByDescending(x => x.Date).First();

Console.WriteLine($"System: {status.Network.Hostname}");
Console.WriteLine($"  PH: {ph?.Value}");
Console.WriteLine($"  Temp: {temp?.Value}");
Console.WriteLine($"  ATO: {ato.GetStatus()} \n\t ATO Low: {atoLo.GetWaterLevel()}\t ATO High: {atoHi.GetWaterLevel()}");
Console.WriteLine($"  Alk: {alk.Value} at {alk.Date.ToNzDateTimeOffSet().ToString("f")}");
Console.WriteLine($"  Calcium: {calc.Value} at {calc.Date.ToNzDateTimeOffSet().ToString("f")}");
Console.WriteLine($"  Magnesium: {mg.Value} at {mg.Date.ToNzDateTimeOffSet().ToString("f")}");