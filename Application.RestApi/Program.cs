using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace Application.RestApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //ELK Loglama icin acilacak
            //ConfigureLogging();
            //then create the host, so that if the host fails we can log errors
            CreateHost(args);
        }
        private static void CreateHost(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (System.Exception ex)
            {
                //Log.Fatal($"Failed to start {Assembly.GetExecutingAssembly().GetName().Name}", ex);
                throw;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
       Host.CreateDefaultBuilder(args)
       .ConfigureWebHostDefaults(webBuilder =>
       {
           webBuilder.UseStartup<Startup>().ConfigureKestrel(options =>
           {

           });

       });
        //ELK Loglama icin acilacak
        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //Host.CreateDefaultBuilder(args)
        //.ConfigureWebHostDefaults(webBuilder =>
        //{
        //    webBuilder.UseStartup<Startup>().ConfigureKestrel(options =>
        //    {

        //    });

        //}).ConfigureAppConfiguration(configuration =>
        //{
        //    configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        //    configuration.AddJsonFile(
        //        $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
        //        optional: true);
        //})
        //.UseSerilog();


        //private static void ConfigureLogging()
        //{
        //    var configuration = new ConfigurationBuilder()
        //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //    .AddJsonFile(
        //    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
        //    optional: true)
        //    .Build();
        //    ElkLogConfigurator.Configure(new Uri(configuration["ElasticConfiguration:Uri"]));
        //}
    }
}
