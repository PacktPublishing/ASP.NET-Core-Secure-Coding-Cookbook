using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OnlineBankingApp.Models;
using System;
using System.Security.Authentication;

namespace OnlineBankingApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    SeedData.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(host =>
                {
                    host.UseKestrel(options =>
                    {
                        options.ConfigureHttpsDefaults(https =>
                        {
                            https.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13;
                        });
                    });                    
                    host.UseStartup<Startup>();
                });
    }
}