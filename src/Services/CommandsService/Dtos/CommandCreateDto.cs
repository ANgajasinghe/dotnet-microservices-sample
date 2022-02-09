using System.ComponentModel.DataAnnotations;
using CommandsService.Entities;
using CommandsService.Mappings;

namespace CommandsService.Dtos;

public class CommandCreateDto : IMapTo<Command>
{
    [Required]
    public string HowTo { get; set; }

    [Required]
    public string CommandLine { get; set; }
}