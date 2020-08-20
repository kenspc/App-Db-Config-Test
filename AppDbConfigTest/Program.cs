using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDbConfigTest.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AppDbConfigTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration(AddConfiguration);

        private static void AddConfiguration(HostBuilderContext context,
            IConfigurationBuilder builder)
        {
            var configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.AddDbConfiguration(options => options.UseMySql(connectionString));
        }
    }
}
