using DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;

namespace RestApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                using (var scope = host.Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                    if (Array.Exists(args, element => element.Equals("recreateDb")))
                        db.Database.EnsureDeleted();
                    db.Database.Migrate();
                }

                host.Run();
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog();
    }
}
