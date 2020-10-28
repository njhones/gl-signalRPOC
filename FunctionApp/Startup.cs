using FunctionApp;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

[assembly: FunctionsStartup(typeof(Startup))]
namespace FunctionApp
{
    public class Startup : FunctionsStartup
    {
        // override configure method
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //var config = new ConfigurationBuilder()
            //   .SetBasePath(Environment.CurrentDirectory)
            //   .AddJsonFile("local.settings.json", true)
            //   .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
            //   .AddEnvironmentVariables()
            //   .Build();

            // register your other services
        }
    }
}
