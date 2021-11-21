using PlatformService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PlatformService.Persistence
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProduction) 
        {
            using (var serviceScope = app.ApplicationServices.CreateScope()) 
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>() ?? throw new ArgumentNullException(nameof(app)), isProduction);
            }
        }


        private static void SeedData(AppDbContext appDbContext, bool isProduction) 
        {
            if (isProduction)
            {
                Console.WriteLine("---> Attempting to apply migrations ....");
                try
                {
                    appDbContext.Database.Migrate();
                }
                catch (Exception e)
                {
                    Console.WriteLine(" ---> could not run migration ");
                    Console.WriteLine(e.Message);
                    
                }
            }

            if (!appDbContext.Platforms.Any()) 
            {
                Console.WriteLine("---> Seeding data ....");
                appDbContext.Platforms.AddRange(
                    new Platform() { Name = "Dot net", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
                    );
                appDbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("---> We already have data");
            }
        }
    }
}
