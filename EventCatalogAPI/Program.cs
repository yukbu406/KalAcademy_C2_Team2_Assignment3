﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EventCatalogAPI.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EventCatalogAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var host = BuildWebHost(args);
           using (var scope = host.Services.CreateScope())
           {
               var services = scope.ServiceProvider;
               var context = 
                   services.GetRequiredService<CatalogContext>();
               CatalogSeed.SeedAsync(context).Wait();
            }
           host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
