﻿using System.ComponentModel.DataAnnotations;

namespace CommandsService.Entities;
public class Platform
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public int ExternalID { get; set; }

    [Required]
    public string Name { get; set; }

    public ICollection<Command> Commands { get; set; } = new List<Command>();
}

