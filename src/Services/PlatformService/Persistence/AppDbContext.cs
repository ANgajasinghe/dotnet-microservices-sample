using Microsoft.EntityFrameworkCore;
using PlatformService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.Persistence;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions options) : base(options) { }
    

    public DbSet<Platform> Platforms{  get; set;}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}

