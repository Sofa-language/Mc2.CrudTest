using Microsoft.AspNetCore.ResponseCompression;
using Mc2.CrudTest.Presentation.Server.Extensions;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using Mc2.CrudTest.Presentation.Server;
using Microsoft.Extensions.Hosting;
using System;

namespace Mc2.CrudTest.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Debugger.Break();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}