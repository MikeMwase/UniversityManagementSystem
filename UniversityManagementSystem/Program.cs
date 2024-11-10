using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityManagementSystem.Data;

namespace UniversityManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
          var host = CreateHostBuilder(args).Build();

          CreateDbIfNotExist(host);

          host.Run();
        }

        private static void CreateDbIfNotExist(IHost host)
        {
            using(var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<UniversityContext>();
                    DbInitializer.Initializer(context);
                }
                catch (Exception ex) 
                {
                   var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex,"Error occurred while creating DB");
                }
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
