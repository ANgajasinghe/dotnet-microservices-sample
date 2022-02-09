using CommandsService.Persistence.Repositories;

namespace CommandsService.Apis;

public static class PlatformEndpoints
{
    public static void MapPlatformEndpoints(this WebApplication app, string baseUrl)
    {
        app.MapGet(baseUrl+"/platform", GetPlatforms);
    }
    
    // platform 
    private static IResult GetPlatforms(ICommandRepo commandRepo)
    {
        Console.WriteLine("--> GetPlatforms calling");
        return Results.Ok(commandRepo.GetAllPlatforms());
    }
}