using PlatformService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.Persistence
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app) 
        {
            using (var serviceScope = app.ApplicationServices.CreateScope()) 
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>() ?? throw new ArgumentNullException(nameof(app)));
            }
        }


        private static void SeedData(AppDbContext appDbContext) 
        {
            if (!appDbContext.Platforms.Any()) 
            {
                Console.WriteLine("---> Seeding data ....");
                appDbContext.Platforms.AddRange(
                    new Platform() { Name = "Dot net", Publisher = "Microsoft", Code = "Free" },
                    new Platform() { Name = "SQL Server Express", Publisher = "Microsoft", Code = "Free" },
                    new Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Code = "Free" }
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
