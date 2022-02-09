using CommandsService.Entities;
using CommandsService.Mappings;

namespace CommandsService.Dtos;

public class PlatformreadDto : IMapFrom<Platform>
{
    public int Id { get; set; }
    public string Name { get; set; }
}