using System.Net;
using BoDi;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using Microsoft.Extensions.Configuration;

namespace Mc2.CrudTest.AcceptanceTests.Hooks;

[Binding]
public class ControllerHooks
{
    private static ICompositeService _compositeService;
    private IObjectContainer _objectRegister;

    public ControllerHooks(IObjectContainer objectRegister)
    {
        _objectRegister = objectRegister;
    }

    [BeforeTestRun]
    public static void DockerComposeUp()
    {
        var containerSql =
            new Builder()
                .UseContainer()
                .WithName("sql")
                .UseImage("mcr.microsoft.com/mssql/server:2019-latest")
                .ExposePort(1434, 1433)
                .WithEnvironment("MSSQL_SA_PASSWORD=Jahan*0021", "SA_PASSWORD=Jahan*0021", "ACCEPT_EULA=Y", "MSSQL_PID=Evaluation")
                .WaitForMessageInLog("Starting up database 'tempdb'.", TimeSpan.FromSeconds(30))
                .Build()
                .Start();

        //var containerSql =
        //    new Builder().UseContainer()
        //        .WithName("Sql")
        //        .UseImage("mcr.microsoft.com/mssql/server:2019-latest")
        //        .ExposePort(1433, 1434)
        //        .WithEnvironment("--accept-eula=Y")
        //        .WithEnvironment("MSSQL_SA_PASSWORD=Jahan*0021")
        //        .WithEnvironment("MSSQL_PID=Evaluation")
        //        .WaitForPort("1434/tcp", 30000 /*30s*/)
        //        .Build()
        //        .Start();

        var containerTest =
            new Builder()
                .UseContainer()
                .WithName("test")
                .UseImage("mc2crudtestpresentationserver")
                .ExposePort(5497, 80)
                .ExposePort(4434, 443)
                .Build()
                .Start();


        //var dockerComposeFilePath = GetDockerComposeLocation();

        //var confirmationUrl = "http://localhost:54977";
        //_compositeService = new Builder()
        //    .UseContainer()
        //    .UseCompose()
        //    .FromFile(dockerComposeFilePath)
        //    .RemoveOrphans()
        //    .WaitForHttp("mc2.crudtest.presentation.server", $"{confirmationUrl}/customers",
        //        continuation: (response, _) => response.Code != HttpStatusCode.OK ? 2000 : 0)
        //    .Build().Start();
    }

    [AfterTestRun]
    public static void DockerComposeDown()
    {
        _compositeService.Stop();
        _compositeService.Dispose();
    }


    [BeforeScenario]
    public void AddHttpClient()
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5497/")
        };
        _objectRegister.RegisterInstanceAs(httpClient);
    }

    private static string GetDockerComposeLocation()
    {
        var directory = Directory.GetCurrentDirectory();
        while (!Directory.EnumerateFiles(directory, "*.yml").Any(x => x.EndsWith("docker-compose.yml")))
        {
            directory = directory.Substring(0, directory.LastIndexOf(Path.DirectorySeparatorChar));
        }

        return Path.Combine(directory, "docker-compose.yml");
    }
}