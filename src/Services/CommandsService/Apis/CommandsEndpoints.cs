using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Entities;
using CommandsService.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Apis;

public static class CommandsEndpoints
{
    public static void MapCommandEndpoints(this WebApplication app, string baseUrl)
    {
        app.MapGet(baseUrl + "/platforms/{platformId}/command", GetCommandsForPlatform);
        app.MapGet(baseUrl + "/platforms/{platformId}/command/{commandId}", GetCommandForPlatform);
        app.MapPost(baseUrl + "/platforms/{platformId}/command", CreateCommandForPlatform);
    }

    private static IResult GetCommandsForPlatform(ICommandRepo commandRepo,
        IMapper mapper,
        int platformId)
    {
        Console.WriteLine($"--> Hit GetCommandsForPlatform {platformId}");
        if (!commandRepo.PlatformExits(platformId)) return Results.NotFound();

        var commands = commandRepo.GetCommandsForPlatform(platformId);
        return Results.Ok(mapper.Map<List<CommandReadDto>>(commands));
    }

    private static IResult GetCommandForPlatform(
        ICommandRepo commandRepo,
        IMapper mapper,
        int platformId,
        int commandId)
    {
        Console.WriteLine($"--> Hit GetCommandsForPlatform {platformId} / {commandId}");
        if (!commandRepo.PlatformExits(platformId)) return Results.NotFound();

        var command = commandRepo.GetCommand(platformId, commandId);

        return command is null
            ? Results.NotFound()
            : Results.Ok(mapper.Map<CommandReadDto>(command));
    }


    private static IResult CreateCommandForPlatform(
        ICommandRepo commandRepo,
        IMapper mapper,
        int platformId,
        [FromBody] CommandCreateDto commandCreateDto
    )
    {
        Console.WriteLine($"--> Hit GetCommandsForPlatform {platformId}");
        if (!commandRepo.PlatformExits(platformId)) return Results.NotFound();

        var command = mapper.Map<Command>(commandCreateDto);

        commandRepo.CreateCommand(platformId, command);
        commandRepo.SaveChanges();

        return Results.CreatedAtRoute(nameof(GetCommandForPlatform),
            new
            {
                platformId,
                commandId = command.Id
            },
            mapper.Map<CommandCreateDto>(command));
    }
}