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
        var dockerComposeFilePath = GetDockerComposeLocation();

        var confirmationUrl = "http://localhost:54977/";
        _compositeService = new Builder()
            .UseContainer()
            .UseCompose()
            .FromFile(dockerComposeFilePath)
            .RemoveOrphans()
            .WaitForHttp("api", $"{confirmationUrl}/customers",
                continuation: (response, _) => response.Code != HttpStatusCode.OK ? 2000 : 0)
            .Build().Start();
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
            BaseAddress = new Uri("http://localhost:54977/")
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