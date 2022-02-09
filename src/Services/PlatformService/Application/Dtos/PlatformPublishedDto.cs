using PlatformService.Application.Mappings;

namespace PlatformService.Application.Dtos;

public class PlatformPublishedDto : IMapFrom<PlatformReadQuery>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Event { get; set; }
}