using PlatformService.Application.Interfaces;
using PlatformService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.Persistence.Repositories
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext _appDbContext;

        public PlatformRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void CreatePlatform(Platform platform)
            => _ = platform == null ? throw new ArgumentNullException(nameof(platform), "Platform cannot be null")
               : _appDbContext.Platforms.Add(platform);
        
        public IEnumerable<Platform> GetAllPlatforms()
            => _appDbContext.Platforms.ToList();

        public Platform GetPlatformById(int id)
            => _appDbContext.Platforms.FirstOrDefault(x => x.Id.Equals(id)) ?? throw new ArgumentNullException(nameof(id),"Invalid Id");

        public bool SaveChanges()
            => (_appDbContext.SaveChanges() >= 0);
        
    }
}
