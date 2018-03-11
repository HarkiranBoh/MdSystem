using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MedicalSystem.Models;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalSystem
{
    public class Program
    {
        //entry point
        //this method takes command line arguments. ASP.NET core application is structured as a console mode application. Can be run from the command line
        //this will build a web hosy passing in any arguments from the commnad line and tell the web server to start running.
        //Buildwebhost is put behind IIS web server. This acts as a proxy server - foward request into the application
        public static void Main(string[] args)
        {
            //BuildWebHost(args).Run();

            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<AppDbContext>();
                    EquipmentDBInitialiser.ProductInformation(context);
                }
                catch(Exception)
                {}
            }
            host.Run();
        }

        //this application has its own server - seperate process that is up and running
        //web server is configured inside of build web host.
        //this is done using a class from microsost.aspNetCore.Hosting
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            //web host builder to look at startup class to configure how the application should behave 
                .UseStartup<Startup>()
                .Build();
        //after config the web host builder will build itslef, which gives back an Iwebhost and tell the web host to start running and listening for http/s connections
    }
}
