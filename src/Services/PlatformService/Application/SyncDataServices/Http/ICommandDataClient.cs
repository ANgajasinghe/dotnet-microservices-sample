using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Formatters;
using PlatformService.Application.Dtos;

namespace PlatformService.Application.SyncDataServices.Http;

public interface ICommandDataClient
{
    Task SendPlatformToCommand(PlatformReadQuery platformReadQuery);
}

public class HttpCommandDataClient : ICommandDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public HttpCommandDataClient(HttpClient httpClient,IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task SendPlatformToCommand(PlatformReadQuery platformReadQuery)
    {
        var httpContent = new StringContent(JsonSerializer.Serialize(platformReadQuery), 
            Encoding.UTF8, 
            "application/json");

        var response = await _httpClient.PostAsync($"{_configuration["CommandService"]}/c/platforms",null);
        
        if(response.IsSuccessStatusCode)
            Console.WriteLine("---> sync POST to command service is ok");
    }
}