using BoDi;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;

namespace Mc2.CrudTest.AcceptanceTests.Hooks;

[Binding]
public class ControllerHooks
{
    private static IContainerService _sqlContainerService;
    private static IContainerService _testContainerService;
    private static INetworkService _networkService;
    private IObjectContainer _objectRegister;

    public ControllerHooks(IObjectContainer objectRegister)
    {
        _objectRegister = objectRegister;
    }

    [BeforeTestRun]
    public static void DockerComposeUp()
    {
        var netwrokName = Guid.NewGuid().ToString();
        _networkService = new Builder().UseNetwork(netwrokName).Build();
        _networkService.Start();

        _sqlContainerService =
            new Builder()
                .UseContainer()
                .WithName("sql1")
                .UseImage("mcr.microsoft.com/mssql/server:2019-latest")
                .ExposePort(1434, 1433)
                .WithEnvironment("MSSQL_SA_PASSWORD=Jahan*0021", "SA_PASSWORD=Jahan*0021", "ACCEPT_EULA=Y", "MSSQL_PID=Evaluation")
                .WaitForMessageInLog("Starting up database 'tempdb'.", TimeSpan.FromSeconds(30))
                .UseNetwork(netwrokName)
                .Build()
                .Start();

        _testContainerService =
            new Builder()
                .UseContainer()
                .WithName("test1")
                .UseImage("mc2crudtestpresentationserver")
                .WithEnvironment("ConnectionStrings:DefaultConnection=Server=sql1;Database=Sample;UID=SA;Password=Jahan*0021")
                .ExposePort(5497, 80)
                .ExposePort(4434, 443)
                .UseNetwork(netwrokName)
                .Build()
                .Start();
    }

    [AfterTestRun]
    public static void DockerComposeDown()
    {
        _sqlContainerService.Stop();
        _sqlContainerService.Dispose();

        _testContainerService.Stop();
        _testContainerService.Dispose();
        
        _networkService.Stop();
        _networkService.Dispose();
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
}