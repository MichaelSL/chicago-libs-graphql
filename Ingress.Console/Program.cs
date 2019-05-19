using Ingress.Core;
using Ingress.Csv;
using Ingress.DataProviders.LiteDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ingress.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create Serilog logger
            Log.Logger = new LoggerConfiguration()
              .Enrich.FromLogContext()
              .WriteTo.Console()
              .CreateLogger();

            //setup our DI
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(loggingBuilder =>
                    loggingBuilder.AddSerilog(dispose: true))
                .AddTransient<IDataProvider, LibrariesLiteDbDataProvider>()
                .AddTransient<IIngressProvider, LibrariesCsvDataProvider>()
                .AddTransient<LibraryParser>()
                .AddTransient<VisitorsParser>()
                .AddTransient<LiteDbRepository>();
                
            
            Log.Debug("Starting application");

            Log.Debug("Initializing configuration");
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                    .AddJsonFile("appsettings.json", optional: true);
                IConfigurationRoot configuration = builder.Build();
                serviceCollection.AddSingleton<IConfiguration>(configuration);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Configuration initialization failed");
                return;
            }


            IDictionary<string, object> parsedArgs = null;
            try
            {
                parsedArgs = new Arguments(args).Data.ToDictionary(_ => _.Key, _ => (object)_.Value);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Cannot parse commmand line arguments");
                return;
            }

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var ingressProvider = serviceProvider.GetService<IIngressProvider>();

            var processingResult = ingressProvider.ProcessData(parsedArgs);

            Log.Debug("All done!");
        }
    }
}
