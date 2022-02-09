using CommandsService.Entities;
using CommandsService.Mappings;

namespace CommandsService.Dtos;

public class CommandReadDto : IMapFrom<Command>
{
    public int Id { get; set; }
    public string HowTo { get; set; }
    public string CommandLine { get; set; }
    public int PlatformId { get; set; }
}