using PlatformService.Application.Mappings;
using PlatformService.Entities;

namespace PlatformService.Application.Dtos
{
    public class PlatformReadQuery : IMapFrom<Platform>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Publisher { get; set; }
        public string? Cost { get; set; }
    }
}
